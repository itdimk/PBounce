using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetsPoolX : MonoBehaviour
{
    public int ScanInterval = 4;

    private readonly Dictionary<string, List<GameObject>> _objects = new Dictionary<string, List<GameObject>>();
    private float _startTick;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= _startTick + ScanInterval)
        {
            _startTick = Time.time;
            RefreshGameObjects();
        }
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
        {
            _objects.Add(targetTag, new List<GameObject>());
            RefreshGameObjects();
        }
        else
            _objects[targetTag].RemoveAll(o => o.gameObject == null);

        return _objects[targetTag];
    }
}