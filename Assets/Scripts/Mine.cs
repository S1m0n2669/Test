using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorldObject
{
    public static List<Mine> MineList = new List<Mine>();

    protected int resourceCapacity;
    protected int workersCapacity;
    protected int currentResourceCount;
    protected int currentWorkersCount;

    public Mine(Vector2 mapSize)
    {
        InitMine(mapSize);
    }

    public Mine(Vector2 mapSize, GameObject mineGO)
    {
        InitMine(mapSize);
        CreateWorldObject(mineGO, Position);
    }

    private void InitMine(Vector2 mapSize)
    {
        this.resourceCapacity = Config.Instance.MineResourceCapacity;
        this.workersCapacity = Config.Instance.WorkerCapacity;

        currentResourceCount = resourceCapacity;

        Position = new Vector2(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));

        Debug.Log("Create Mine with coord: " + Position);

        MineList.Add(this);
    }

    public bool TryAddWorker()
    {
        if (currentWorkersCount < workersCapacity)
        {
            currentWorkersCount++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WorkerOut()
    {
        currentWorkersCount--;
    }

    /// <summary>
    /// Добывать ресурсы
    /// </summary>
    /// <param name="haveToMineResourceCount">Сколько ресурсов хочет добывать рабочий</param>
    /// <returns>Сколько удалось в итоге добыть с шахты</returns>
    public int MineResource(int haveToMineResourceCount)
    {
        if (haveToMineResourceCount >= currentResourceCount)
        {
            Remove();
            return currentResourceCount;
        }
        else
        {
            currentResourceCount -= haveToMineResourceCount;
            return haveToMineResourceCount;
        }
    }

    public override void Remove()
    {
        base.Remove();
        WorldGenerator.Instance.RemoveMine(this);
    }

    public override string GetData()
    {
        return base.GetData();
    }
}
