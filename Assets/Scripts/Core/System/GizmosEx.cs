using UnityEngine;

public static class GizmosEx
{
    public static void DrawRect(Rect rect)
    {
        Gizmos.DrawLine(rect.min, new Vector2(rect.xMin, rect.yMax));
        Gizmos.DrawLine(rect.min, new Vector2(rect.xMax, rect.yMin));
        Gizmos.DrawLine(rect.max, new Vector2(rect.xMax, rect.yMin));
        Gizmos.DrawLine(rect.max, new Vector2(rect.xMin, rect.yMax));
    }
}