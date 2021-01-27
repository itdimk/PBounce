using System.Collections.Generic;
using UnityEngine;

public class TargetsPool : MonoBehaviour
{
    public int ScanInterval = 4;

    private readonly Dictionary<string, List<GameObject>> _objects = new Dictionary<string, List<GameObject>>();
    
    // Update is called once per frame
    void Update()
    {
        if (ActionEx.CheckCooldown(Update, ScanInterval))
            RefreshGameObjects();
    }

    void RefreshGameObjects()
    {
        foreach (var key in _objects.Keys)
        {
            _objects[key].Clear();
            _objects[key].AddRange(GameObject.FindGameObjectsWithTag(key));
        }
    }

    public List<GameObject> GetObjectsByTagNotNull(string targetTag)
    {
        if (!_objects.ContainsKey(targetTag))
            _objects.Add(targetTag, new List<GameObject>());
        else
            _objects[targetTag].RemoveAll(o => o.gameObject == null);

        return _objects[targetTag];
    }
}