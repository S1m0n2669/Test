using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : SingletonObject <UIcontroller>
{
    public ResourceUpdate resourceUpdate;

    public delegate void CreateWorld();
    public CreateWorld createWorld;

    public delegate void Create3DWorld();
    public Create3DWorld create3DWorld;

    public delegate void CreateNewStorage();
    public CreateNewStorage createNewStorage;
    
    public delegate void RemoveStorage();
    public RemoveStorage removeStorage;

    public delegate void StartThread();
    public StartThread startThread;

    public delegate void StopThread();
    public StopThread stopThread;

    DateTime LastUpdateResourceCountTime;

    void Start()
    {
        ResourceCounter.Instance.resourceUpdate += UpdateResourceCountText;
    }

    void UpdateResourceCountText()
    {
        resourceUpdate.CountUpdate();
    }

    public void OnClickCreateWorld()
    {
        createWorld?.Invoke();
    }

    public void OnClickCreate3DWorld()
    {
        create3DWorld?.Invoke();
    }

    public void OnClickCreateNewStorage()
    {
        createNewStorage?.Invoke();
    }

    public void OnClickRemoveStorage()
    {
        removeStorage?.Invoke();
    }

    public void OnClickStartThread()
    {
        startThread?.Invoke();
    }

    public void OnClickStopThread()
    {
        stopThread?.Invoke();
    }


}
