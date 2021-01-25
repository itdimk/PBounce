using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementStats))]
public class JumpEffectSpawner : MonoBehaviour
{
    public GameObject TargetToSpawn;

    private Vector2 _spawnAt;
    private MovementStats _stats;
    private Vector2 _lastSpawnedPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<MovementStats>();
    }

    // Update is called once per frame
    void OnCollisionExit2D(Collision2D col)
    {
        if (_lastSpawnedPoint != _spawnAt)
        {
            Instantiate(TargetToSpawn, _spawnAt, Quaternion.identity);
            _lastSpawnedPoint = _spawnAt;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_stats.WhatIsGround != (_stats.WhatIsGround | (1 << other.gameObject.layer))) return;
        
        if (other.contacts.Length > 0)
            _spawnAt = other.contacts[0].point;
        

    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (_stats.WhatIsGround != (_stats.WhatIsGround | (1 << other.gameObject.layer))) return;
        
        if (other.contacts.Length > 0)
            _spawnAt = other.contacts[0].point;
    }
}
