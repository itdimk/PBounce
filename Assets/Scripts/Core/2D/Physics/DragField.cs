using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DragField : MonoBehaviour
{
    public float DragValue = 10;
    public string[] IgnoredTags = { };

    private Dictionary<int, float> _defaultDrag = new Dictionary<int, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
            SetDrag(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
            RestoreDrag(other.gameObject);
    }

    private void SetDrag(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D physics))
        {
            _defaultDrag[obj.GetInstanceID()] = physics.drag;
            physics.drag = DragValue;
        }
    }

    private void RestoreDrag(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D physics))
            physics.drag = _defaultDrag[obj.GetInstanceID()];
    }
}