using UnityEngine;

public class GridLayout : MonoBehaviour
{
    public int Columns;
    public float ColumnGap;
    public float RowGap;
    public bool DependOnScreenSize;

    private bool _initialized;


    private Vector2 GetPoint(int row, int col)
    {
        return  transform.right * ColumnGap * col - transform.up * RowGap * row;
    }

    private void OnTransformChildrenChanged()
    {
        if (!_initialized && DependOnScreenSize)
        {
            ColumnGap = ColumnGap * Screen.width / 100f;
            RowGap = RowGap * Screen.height / 100f;
            _initialized = true;
        }

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