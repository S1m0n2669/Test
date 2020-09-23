using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3DController : MonoBehaviour
{
    WorldObject worldObject;
    bool NeedDestroy = false;

    public void SetWorldObject(WorldObject worldObject)
    {
        this.worldObject = worldObject;
    }

    public void Destroy()
    {
        NeedDestroy = true;
    }

    private void Update()
    {
        transform.position = new Vector3(worldObject.Position.x, 0, worldObject.Position.y);

        if (NeedDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
