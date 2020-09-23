using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoverer : MonoBehaviour
{
    public List<IRecoveryObject> recoveryObjects;

    void AddObjectToList(IRecoveryObject obj)
    {
        recoveryObjects.Add(obj);

        //obj.GetData();
    }
}
