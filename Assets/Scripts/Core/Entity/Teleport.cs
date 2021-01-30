using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Dictionary<GameObject, float> _teleportDelays
        = new Dictionary<GameObject, float>();

    public Transform Destination;

    public float TeleportDelay;
    public bool HideWithinDelay;
    public string[] TriggerTags = {"Player"};


    // Update is called once per frame
    void Update()
    {
        if (_teleportDelays.Count == 0) return;

        foreach (var obj in _teleportDelays.Keys.ToArray())
        {
            float delay = (_teleportDelays[obj] -= Time.deltaTime);

            if (delay <= 0)
                EndTeleport(obj);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TriggerTags.Contains(other.tag) && !_teleportDelays.ContainsKey(other.gameObject))
            BeginTeleport(other.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.tag) && !_teleportDelays.ContainsKey(other.gameObject))
            BeginTeleport(other.gameObject);
    }

    private void BeginTeleport(GameObject target)
    {
        _teleportDelays.Add(target, TeleportDelay);

        if (HideWithinDelay)
            target.SetActive(false);
    }

    private void EndTeleport(GameObject target)
    {
        _teleportDelays.Remove(target);
        target.transform.position = Destination.position;

        if (HideWithinDelay)
            target.SetActive(true);
    }
}