using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    public TargetProviderBaseX Destination;

    public UnityEvent BeforeTeleport;
    public UnityEvent AfterTeleport;

    public string[] TriggerTags = { "Player" };

    public float Delay;

    private float _teleportTick;
    private bool _teleporting;
    private GameObject _objectToTeleport;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TeleportIfRequired();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TriggerTags.Contains(other.tag) && !_teleporting)
        {
            BeforeTeleport?.Invoke();
            _teleporting = true;
            _teleportTick = Time.time;
            _objectToTeleport = other.gameObject;
        }
    }

    private void TeleportIfRequired()
    {
        if (_teleporting && Time.time - _teleportTick >= Delay)
        {
            _objectToTeleport.transform.position = Destination.GetTarget().position;
            if (_objectToTeleport.TryGetComponent(out Rigidbody2D physics))
            {
                physics.velocity = Vector2.zero;
            }
            
            _teleporting = false;
            AfterTeleport?.Invoke();
          
        }
    }
}
