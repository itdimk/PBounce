using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool _activated;
    public float ActivatedAt { get; private set; }
    public bool IsActivated { get; private set; }


    private void Start()
    {
        IsActivated = _activated;
    }

    public void Activate()
    {
        IsActivated = true;
        ActivatedAt = Time.time;
    }
}