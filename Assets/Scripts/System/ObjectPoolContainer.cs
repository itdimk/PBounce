using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.Init(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
