using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : WorldObject
{
    enum WorkerState
    {
        stop = 0,
        movingToMine = 1,
        movingToStorage = 2,
        mining = 3
    }

    WorkerState currentState;

    int capacity;
    float miningSpeed;
    float movementSpeed;

    Storage myStorage;
    Mine targetMine;

    Vector2 Direction;

    int resourceCount; //сколько ресурсов несёт с собой рабочий 

    List<Mine> bannedMineList; //шахты, в которые рабочий не будет ходить, потому что там его не пропустили

    public Worker(Storage storage, GameObject worker3DPref)
    {
        Debug.Log("Worker created!");

        bannedMineList = new List<Mine>();

        this.capacity = Config.Instance.Capacity;
        this.miningSpeed = Config.Instance.MiningSpeed;
        this.movementSpeed = Config.Instance.MovementSpeed;

        myStorage = storage;
        Position = storage.Position;

        targetMine = FindClosestMine(bannedMineList);

        if (targetMine != null)
        {
            currentState = WorkerState.movingToMine;
            Direction = (targetMine.Position - Position).normalized;
        }
        else
        {
            Debug.Log("No any Mine for mining");
        }

        CreateWorldObject(worker3DPref, storage.Position);
    }

    private Mine FindClosestMine(List <Mine> bannedMineList)
    {
        float shortestDistance = Mathf.Infinity;
        Mine closestMine = null;

        foreach (var mine in Mine.MineList)
        {
            if (mine == null || !mine.isActive || bannedMineList.Contains(mine))
                continue;

            var Dist = Vector3.Distance(Position, mine.Position);
            if (Dist < shortestDistance)
            {
                shortestDistance = Dist;
                closestMine = mine;
            }
        }

        return closestMine;
    }

    public void WorkerUpdate()
    {
        if (!isActive)
            return;

        switch (currentState)
        {
            case WorkerState.stop:
                return;

            case WorkerState.movingToMine:
                MoveToMine();
                break;

            case WorkerState.movingToStorage:
                MoveToStorage();
                break;

            case WorkerState.mining:
                Mining();
                break;
        }
    }

    void Mining()
    {
        Debug.Log("Mining on = " + Position + "resourceCount = " + resourceCount);

        if (resourceCount >= capacity) //добыл ресурсов сколько нужно
        {
            targetMine.WorkerOut();
            Direction = (myStorage.Position - Position).normalized;
            currentState = WorkerState.movingToStorage;
            targetMine = null;
        }
        else
        {
            if (targetMine != null && targetMine.isActive)
            {
                //из условия задачи не понятно, должен ли рабочий сколько то секунд добывать со скоростью miningSpeed,
                //а потом взять себе сколько то ресурсов одним куском, или же добывать непрерывно, постоянно делая запрос к шахте
                //если второе, что miningSpeed должен быть float?
                resourceCount += targetMine.MineResource((int)miningSpeed);
            }
            else
            {
                targetMine = FindClosestMine(bannedMineList);

                if (targetMine == null)
                    return;

                Direction = (targetMine.Position - Position).normalized;
                currentState = WorkerState.movingToMine;
            }
        }
    }

    void MoveToMine()
    {
        Debug.Log("<color=green>Moving to mine </color>");

        if (targetMine == null || !targetMine.isActive)
        {
            targetMine = FindClosestMine(bannedMineList);

            if (targetMine != null)
            {
                Debug.Log("<color=green>targetMine = " + targetMine + " </color>");
                Direction = (targetMine.Position - Position).normalized;
            }
            else
            {
                currentState = WorkerState.movingToStorage;
            }

            return;
        }

        if (Vector3.Distance(targetMine.Position, Position) < Config.Instance.StopDistance)
        {
            if (targetMine.TryAddWorker())
            {
                currentState = WorkerState.mining;
            }
            else //рабочий не смог подключиться к шахте и пошёл искать следущую
            {
                if (targetMine != null && !bannedMineList.Contains(targetMine))
                {
                    bannedMineList.Add(targetMine);
                }

                targetMine = FindClosestMine(bannedMineList);

                if (targetMine != null)
                {
                    Direction = (targetMine.Position - Position).normalized;
                }
                else
                {
                    currentState = WorkerState.movingToStorage;
                }
            }
        }
        else
        {
            Moving();
        }
    }

    void MoveToStorage()
    {
        Debug.Log("<color=red>Moving to storage</color>");

        if (Vector2.Distance(myStorage.Position, Position) < Config.Instance.StopDistance)
        {
            //todo попытка пройти на склад

            //разгрузка в случае успеха
            ResourceCounter.Instance.AddResources(resourceCount);
            resourceCount = 0;
            currentState = WorkerState.movingToMine;
        }
        else
        {
            Moving();
        }
    }

    private void Moving()
    {
        Position += Direction * movementSpeed;
    }
}
