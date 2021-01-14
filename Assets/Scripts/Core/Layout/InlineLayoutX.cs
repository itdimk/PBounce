using System;
using UnityEngine;

public class InlineLayoutX : MonoBehaviour
{
    public float Gap;

    // Start is called before the first frame update
    void Start()
    {
        OnTransformChildrenChanged();
    }

    private Vector2 GetPoint(int index)
    {
        return transform.position + transform.right * (Gap * index);
    }

    private void OnDrawGizmos()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = GetPoint(5 - 1);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(startPoint, endPoint);

        for (int i = 0; i < 5; ++i)
            Gizmos.DrawWireSphere(GetPoint(i), Gap / 4f);
    }

    private void OnTransformChildrenChanged()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            var child = transform.GetChild(i);
            child.position = GetPoint(i);
        }
        
        Debug.Log(childCount);
    }
}
