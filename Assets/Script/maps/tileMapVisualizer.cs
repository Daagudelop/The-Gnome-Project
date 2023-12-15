using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap,wallTileMap;

    [SerializeField] private TileBase floorTile, wallTopEmpty, wallTopA, wallTopB, wallTopC,
        wallLeft, wallBottom, wallRight,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, 
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

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
    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = floorTile;
        }
        else if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTopA;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallLeft;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallRight;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile == floorTile)
        {
            PaintSingleTile(floorTileMap, tile, position);
        }
        if (tile != null)
        {
            if (tile == wallTopA)
            {
                PaintSingleTile(wallTileMap, tile, position);
                //PaintSingleTile(wallTileMap, wallTopB, position + new Vector2Int(0, -1));
                //PaintSingleTile(wallTileMap, wallTopC, position + new Vector2Int(0, -2));
            }
            else if(tile != wallTopA || tile != floorTile)
            {
                PaintSingleTile(wallTileMap, tile, position);
            }
            
        }
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

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) 
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = floorTile;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile == floorTile)
        {
            PaintSingleTile(floorTileMap, tile, position);
        }
        else if (tile != null && tile != floorTile)
        {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }
}
