using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : IRecoveryObject
{
    public bool isActive { get; protected set; }
    protected Object3DController object3DController;

    protected GameObject object3D = null;

    public Vector2 Position { get; protected set; } 

    public void CreateWorldObject(GameObject Pref, Vector2 coord)
    {
        if (Pref == null)
            return;

        object3D = WorldGenerator.Instance.CreateWorldObject(Pref);

        isActive = true;
        object3D.transform.position = new Vector3(coord.x, 0, coord.y);

        object3DController = object3D.GetComponent<Object3DController>();
        object3DController.SetWorldObject(this);
    }

    public virtual void Remove()
    {
        isActive = false;

        if (object3DController != null)
            object3DController.Destroy();
    }

    public virtual string GetData()
    {
        return "";
    }
}
