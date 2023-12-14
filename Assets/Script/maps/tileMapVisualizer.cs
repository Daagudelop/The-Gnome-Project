using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap,wallTileMap;

    [SerializeField] private TileBase floorTile, walltop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, Tile, position);
        }
    }
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap,walltop, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}
