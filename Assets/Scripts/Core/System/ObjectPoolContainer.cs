using UnityEngine;

public class ObjectPoolContainer : MonoBehaviour
{
    void Start()
    {
        if(transform.childCount > 0)
            Debug.LogWarning($"Object pool container \"{gameObject.name}\" shouldn't have any children");
        ObjectPool.Init(transform);
    }
}
