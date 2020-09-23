using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUpdate : MonoBehaviour
{
    public Text ResourceTextCount = null;
    bool needUpdate = false;

    public void CountUpdate()
    {
        needUpdate = true;
    }

    private void Update()
    {
        if (needUpdate)
        {
            ResourceTextCount.text = ResourceCounter.Instance.ResourceCount.ToString();
            needUpdate = false;
        }
    }
}
