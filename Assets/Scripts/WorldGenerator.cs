using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : SingletonObject<WorldGenerator>
{
    public GameObject StoragePref;
    public GameObject MinePref;
    public GameObject WorkerPref;

    public Vector2 MapSize;

    List<Mine> MineList = new List<Mine>();
    public List<Storage> StorageList = new List<Storage>();

    WorkerManager workerManager = null;

    enum WorldType
    {
        none = 0,
        noVisual = 1,
        visual3d = 2
    }

    WorldType worldType = WorldType.none;

    void Start()
    {
        //base.Awake();
        UIcontroller.Instance.createWorld += CreateWorld;
        UIcontroller.Instance.create3DWorld += Create3DWorld;
        UIcontroller.Instance.createNewStorage += CreateNewStorage;
        UIcontroller.Instance.removeStorage += RemoveStorage;
    } 

    void CreateWorld()
    {
        if (worldType != WorldType.none)
        {
            Debug.Log("World alredy created");
            return;
        }

        worldType = WorldType.noVisual;

        for (int i=0; i < Config.Instance.LimitMineMax; i++)
        {
            Mine.MineList.Add(new Mine(MapSize));
        }

        Debug.Log("World created!");
    }

    void Create3DWorld()
    {
        if (worldType != WorldType.none)
        {
            Debug.Log("World alredy created");
            return;
        }

        worldType = WorldType.visual3d;

        for (int i = 0; i < Config.Instance.LimitMineMax; i++)
        {
            MineList.Add(new Mine(MapSize, MinePref));
        }

        Debug.Log("World 3D created!");
    }

    void CreateNewStorage()
    {
        if(workerManager == null)
           workerManager = new WorkerManager();

        Vector2 position = new Vector2(Random.Range(0, MapSize.x), Random.Range(0, MapSize.y));

        if (worldType == WorldType.none)
        {
            Debug.Log("Need create world before");
            return;
        }

        StorageList.Add(new Storage(position, worldType == WorldType.visual3d ? StoragePref : null, WorkerPref));
    }

    public GameObject CreateWorldObject(GameObject Pref)
    {
        return Instantiate(Pref);
    }

    public void RemoveMine(Mine mine)
    {
        MineList.Remove(mine);
    }

    public void RemoveStorage()
    {
        var tmpStorage = StorageList[Random.Range(0, StorageList.Count)];
        tmpStorage.Remove();

        StorageList.Remove(tmpStorage);
    }
}
