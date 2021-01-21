using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGen : MonoBehaviour
{
    public Tilemap Target;
    public Tile[] Tiles;
    public RectInt NextPlatformOffset;

    public int Depth = 1;

    // Start is called before the first frame update
    void Start()
    {
        Generate( Depth);
    }
    
    public void Generate(int depth)
    {
        if (depth == 0) return;
        var pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        
        Target.SetTile(pos, GetRandomTile());
        Generate(GetNextPosition(pos), depth - 1);
    }

    void Generate(Vector3Int position, int depth)
    {
        if (depth == 0) return;
        Target.SetTile(position, GetRandomTile());
        Generate(GetNextPosition(position), depth - 1);
    }


    Vector3Int GetNextPosition(Vector3Int from)
    {
        int offsetX = Random.Range(NextPlatformOffset.xMin, NextPlatformOffset.xMax + 1);
        int offsetY = Random.Range(NextPlatformOffset.yMin, NextPlatformOffset.yMax + 1);


        return new Vector3Int(from.x + offsetX, from.y + offsetY, from.z);
    }

    Tile GetRandomTile()
    {
        int index = Random.Range(0, Tiles.Length);
        return Tiles[index];
    }
}