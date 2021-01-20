using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount != 0)
            Debug.LogWarning($"Object pool container \"{gameObject.name}\" shouldn't have children");
        ObjectPool.Init(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
