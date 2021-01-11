using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool _isChecked;
    private float _isCheckedTick;
    
    public bool IsChecked
    {
        get { return _isChecked; }
        set
        {
            _isChecked = value;
            _isCheckedTick = Time.time;
        }
    }

    public float IsCheckedTick
    {
        get => _isCheckedTick;
    }

 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
