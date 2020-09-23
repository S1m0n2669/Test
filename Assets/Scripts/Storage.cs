using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : WorldObject
{
    int GuardCount;
    int LimitWorkerMax;

    public List<Worker> workerList; //todo сделать гетер с рид онли

    public Storage(Vector2 position, GameObject storage3D, GameObject workerPref)
    {
        Debug.Log("Storage created with coord: " + position);

        Position = position;
        workerList = new List<Worker>();

        LimitWorkerMax = Config.Instance.LimitWorkerMax;

        for (int i = 0; i < LimitWorkerMax; i++)
        {
            workerList.Add(new Worker(this, workerPref));
        }

        CreateWorldObject(storage3D, Position);
    }

    public override void Remove()
    {
        foreach (var worker in workerList)
        {
            worker.Remove();
        }

        workerList.Clear();
        base.Remove();
    }
}

