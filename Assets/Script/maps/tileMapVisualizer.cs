using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap,floorFillTileMap, wallTileMap, overWallTileMap;

    [SerializeField] private TileBase floorTile, wallTopEmpty, wallTopA, wallTopB, wallTopC,
        wallLeft, wallBottom, wallRight,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallInnerCornerUpLeftA, wallInnerCornerUpLeftB,
        wallInnerCornerUpRightA, wallInnerCornerUpRightB,


        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, 
        wallDiagonalCornerUpRightA, wallDiagonalCornerUpRightB,
        wallDiagonalCornerUpLeftA, wallDiagonalCornerUpLeftB;

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
        //if (WallTypesHelper.wallFull.Contains(typeAsInt))
        //{
        //    tile = floorTile; 
        //}
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
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
        if (tile != null)
        {
            if (tile == floorTile)
            {
                PaintSingleTile(floorFillTileMap, tile, position);
            }
            else if (tile == wallTopA)
            {
                PaintSingleTile(overWallTileMap, tile, position);
                PaintSingleTile(wallTileMap, wallTopB, position + new Vector2Int(0, -1));
            }
            else
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
        overWallTileMap.ClearAllTiles();
        floorFillTileMap.ClearAllTiles();
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
            tile = wallDiagonalCornerUpLeftA;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRightA;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = floorTile;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallUpEightDirections.Contains(typeAsInt))
        {
            tile = wallTopA;
        }
        else if (WallTypesHelper.wallRightEightDirections.Contains(typeAsInt))
        {
            tile = wallRight;
        }
        else if (WallTypesHelper.wallLeftEightDirections.Contains(typeAsInt))
        {
            tile = wallLeft;
        }

        else if (WallTypesHelper.wallInnerCornerUpRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerUpRightA;
        }
        else if (WallTypesHelper.wallInnerCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerUpLeftA;
        }




        if (tile == floorTile)
        {
            PaintSingleTile(floorFillTileMap, tile, position);
        }
        if (tile != null && tile != floorTile)
        {

            PaintSingleTile(wallTileMap, tile, position);
            if (tile == wallInnerCornerDownRight || tile == wallInnerCornerDownLeft)
            {
                PaintSingleTile(floorFillTileMap, floorTile, position);
                PaintSingleTile(wallTileMap, tile, position);
            }

            else if (tile == wallInnerCornerUpRightA)
            {
                PaintSingleTile(wallTileMap, wallInnerCornerUpRightB, position + new Vector2Int(0,-1));
            }

            else if (tile == wallInnerCornerUpLeftA)
            {
                PaintSingleTile(wallTileMap, wallInnerCornerUpLeftB, position + new Vector2Int(0, -1));
            }

            else if (tile == wallTopA)
            {
                PaintSingleTile(overWallTileMap, tile, position);
                PaintSingleTile(wallTileMap, wallTopB, position + new Vector2Int(0, -1));
            }
            else
            {
                PaintSingleTile(wallTileMap, tile, position);
            }
        }
    }
}
