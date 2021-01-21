using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject Target;

    public float SpawnInterval = 2f;

    public float RandomizePosition = 0.5f;
    public float RandomizeInterval = 0.5f;

    [Space] public int MaxOnlineCount = 2;
    public int TotalCount = 10;

    private List<GameObject> _spawned = new List<GameObject>();

    private float _spawnInterval;
    public UnityEvent BeforeSpawn = new UnityEvent();
    public UnityEvent AfterSpawn = new UnityEvent();


    // Update is called once per frame
    void LateUpdate()
    {
        if (IsSpawnRequired() && ActionEx.CheckCooldown(Spawn, _spawnInterval))
            Spawn();
    }

    bool IsSpawnRequired()
    {
        if (TotalCount <= 0) return false;

        _spawned.RemoveAll(o => o.gameObject == null);
        int onlineCount = _spawned.Count(o => o.activeSelf);
        return onlineCount < MaxOnlineCount;
    }


    public void Spawn()
    {
        BeforeSpawn?.Invoke();

        var obj = Target.GetCloneFromPool(null, GetSpawnPoint(), transform.rotation);
        obj.SetActive(true);

        if (!_spawned.Contains(obj))
            _spawned.Add(obj);

        TotalCount--;
        _spawnInterval = Random.Range(SpawnInterval - RandomizeInterval, SpawnInterval + RandomizeInterval);

        AfterSpawn?.Invoke();
    }

    Vector3 GetSpawnPoint()
    {
        Vector2 currPos = transform.position;

        float randomRadius = Random.Range(-RandomizePosition, RandomizePosition);
        float randomAngle = Random.Range(0, Mathf.PI);

        Vector2 direction = new Vector2(
            randomRadius * Mathf.Cos(randomAngle),
            randomRadius * Mathf.Sin(randomAngle)
        );

        return currPos + direction;
    }
}