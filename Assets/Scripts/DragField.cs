using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DragField : MonoBehaviour
{
    public float Density = 10;
    public string[] IgnoredTags = { };
    
    
    private Dictionary<int, float> DefaultDrag = new Dictionary<int, float>();
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
        {
            SetDrag(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IgnoredTags.Contains(other.tag))
        {
            RestoreDrag(other.gameObject);
        }
    }

    private void SetDrag(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D physics))
        {
            DefaultDrag[obj.GetInstanceID()] = physics.drag;
            physics.drag = Density;
        }
    }

    private void RestoreDrag(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D physics))
        {
            physics.drag = DefaultDrag[obj.GetInstanceID()];
        }
    }
}