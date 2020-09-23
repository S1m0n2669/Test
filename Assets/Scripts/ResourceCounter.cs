using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : SingletonObject<ResourceCounter>
{
    public int ResourceCount { get; private set; }

    public delegate void ResourceUpdate();
    public ResourceUpdate resourceUpdate;

    public void AddResources(int count)
    {
        ResourceCount += count;
        resourceUpdate?.Invoke();
    } 

}
