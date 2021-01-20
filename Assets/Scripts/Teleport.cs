using System.Linq;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform Destination;

    public string[] TriggerTags = {"Player"};
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
            BeginTeleport(other.gameObject);
    }

    private void TeleportIfRequired()
    {
        if (_teleporting && Time.time - _teleportTick >= Delay)
        {
            _objectToTeleport.transform.position = Destination.position;
            EndTeleport();
        }
    }

    private void BeginTeleport(GameObject objectToTeleport)
    {
        _teleporting = true;
        _teleportTick = Time.time;
        _objectToTeleport = objectToTeleport;

        _objectToTeleport.SetActive(false);
        if (objectToTeleport.TryGetComponent(out Rigidbody2D physics))
            physics.simulated = false;
    }

    private void EndTeleport()
    {
        _teleporting = false;
        _objectToTeleport.SetActive(true);


        if (_objectToTeleport.TryGetComponent(out Rigidbody2D physics))
            physics.simulated = true;
    }
}