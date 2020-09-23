using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WorkerManager
{
    Thread WorkingThread = null;

    bool StopFlag;
    EventWaitHandle ChildWaitHandler = new EventWaitHandle(true, EventResetMode.ManualReset);

    public WorkerManager()
    {
        WorkingThread = new Thread(Working);
        WorkingThread.Start();

        UIcontroller.Instance.startThread += StartWorking;
        UIcontroller.Instance.stopThread  += StopWorking;
    }

    void StopWorking()
    {
        Debug.Log("<color=red>StopWorking</color>");
        StopFlag = true;
    }

    void StartWorking()
    {
        Debug.Log("<color=green>StartWorking</color>");
        StopFlag = false;
        ChildWaitHandler.Set();
    }

    void Working()
    {
        ChildWaitHandler.Reset();
        ChildWaitHandler.WaitOne();

        while (true)
        {
            if (StopFlag)
            {
                Debug.Log("Stop workers thread");
                ChildWaitHandler.Reset();
                ChildWaitHandler.WaitOne();
            }

            for (int storageNum = 0; storageNum < WorldGenerator.Instance.StorageList.Count; storageNum ++)
            {
                var CurWorkerList = WorldGenerator.Instance.StorageList[storageNum].workerList;

                for (int i = 0; i < CurWorkerList.Count; i++)
                {
                    if (CurWorkerList[i] == null)
                    {
                        CurWorkerList.RemoveAt(i);
                        i--;
                    }

                    CurWorkerList[i].WorkerUpdate();
                }
            }

            Thread.Sleep(Config.Instance.ThreadSleepTime);
        }
    }

    void OnApplicationQuit()
    {
        StopWorking();
    }
}
