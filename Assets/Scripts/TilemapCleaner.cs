using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCleaner : MonoBehaviour
{
    public int Height;
    public Tilemap Target;

    // Update is called once per frame
    void FixedUpdate()
    {
        var pos = GetPos();
        int startY = pos.y - Height / 2;
        int endY = pos.y + Height / 2;

        for (int y = startY; y <= endY; ++y)
            Target.SetTile(new Vector3Int(pos.x, y, 0), null);
    }

    Vector3Int GetPos()
    {
        return new Vector3Int((int) transform.position.x, (int) transform.position.y, 0);
    }
}