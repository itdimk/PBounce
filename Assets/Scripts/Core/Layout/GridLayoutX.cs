using System;
using UnityEngine;

public class GridLayoutX : MonoBehaviour
{
    public int Columns = 3;
    public float ColumnGap = 1;
    public float RowGap = 1;

    private void Start()
    {
        OnTransformChildrenChanged();
    }

    private Vector2 GetPoint(int row, int col)
    {
        return transform.right * ColumnGap * col - transform.up * RowGap * row;
    }

    private void OnTransformChildrenChanged()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            int r = i / Columns;
            int c = i - r * Columns;

            var child = transform.GetChild(i);
            child.localPosition = GetPoint(r, c);
        }
    }
}