using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    private static Dictionary<string, Stack<GameObject>> _pool;
    private static Transform _deactivatedObjectsParent;

    public static void Init(Transform pooledObjectsContainer)
    {
        _deactivatedObjectsParent = pooledObjectsContainer;

        if (_pool == null)
            _pool = new Dictionary<string, Stack<GameObject>>();
    }

    public static GameObject GetCloneFromPool(this GameObject prefab, Transform parent)
    {
        return parent != null
            ? GetCloneFromPool(prefab, parent, parent.position, parent.rotation)
            : GetCloneFromPool(prefab, null, Vector3.zero, Quaternion.identity);
    }

    public static GameObject GetCloneFromPool(this GameObject prefab, Transform parent, Vector3 position,
        Quaternion rotation)
    {
        GameObject result;

        if (!_pool.TryGetValue(prefab.name, out var stack))
            _pool.Add(prefab.name, stack = new Stack<GameObject>());


        if (stack.Count > 0)
        {
            result = stack.Pop();
            result.transform.SetPositionAndRotation(position, rotation);
            result.transform.parent = parent;
        }
        else
        {
            result = Object.Instantiate(prefab, position, rotation, parent);
        }

        result.name = prefab.name;
        result.SetActive(true);

        return result;
    }

    public static void PutToPool(this GameObject target)
    {
        if (!_pool.TryGetValue(target.name, out var stack))
            _pool.Add(target.name, stack = new Stack<GameObject>());

        stack.Push(target);
        target.transform.parent = _deactivatedObjectsParent;
        target.SetActive(false);
    }
}