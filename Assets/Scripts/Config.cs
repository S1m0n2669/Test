using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : SingletonObject<Config>
{
    [Header("Storage")]
    public int LimitStorageMax = 10;
    public int LimitWorkerMax = 3; //количество рабочих, которое создаёт хранилище, когда появляется

    [Space]
    [Header("Mine")]
    public int LimitMineMax = 10;
    public int MineResourceCapacity = 1000;
    public int WorkerCapacity = 3; //вместимость шахты

    [Space]
    [Header ("Worker")]
    public int Capacity = 10;
    public int MiningSpeed = 10;
    public float MovementSpeed = 10;
    public float StopDistance = 1f;

    [Space]
    [Header("Thread")]
    public int ThreadSleepTime = 100;
}
