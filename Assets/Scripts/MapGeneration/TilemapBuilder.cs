using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBuilder : MonoBehaviour
{
    public Tilemap landTilemap;
    public Tilemap waterTilemap;

    public List<Tile> landTiles;
    public List<Tile> waterTiles;

    public Tile landTileBorder;
    public Tile landTileCorner;
    public Tile landTileInnerCorner;

    public void GenerateIsland(int[,] matrix)
    {
        ClearTilemaps();

        int matrixWidth = matrix.GetLength(1);
        int matrixHeight = matrix.GetLength(0);

        int offsetX = -matrixWidth / 2;
        int offsetY = -matrixHeight / 2;

        for (int y = 0; y < matrixHeight; y++)
        {
            for (int x = 0; x < matrixWidth; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x + offsetX, (matrixHeight - 1 - y) + offsetY, 0);

                if (matrix[y, x] != 0) // Cualquier número positivo es tierra
                {
                    TileBase tileToPlace = GetTileForPosition(matrix, x, y, out float rotationAngle);
                    SetTileWithRotation(landTilemap, tilePosition, tileToPlace, rotationAngle);
                }
                else
                {
                    TileBase waterTileToPlace = waterTiles[Random.Range(0, waterTiles.Count)];
                    waterTilemap.SetTile(tilePosition, waterTileToPlace);
                }
            }
        }
    }

    private void ClearTilemaps()
    {
        landTilemap.ClearAllTiles();
        waterTilemap.ClearAllTiles();
    }

    private TileBase GetTileForPosition(int[,] matrix, int x, int y, out float rotationAngle)
    {
        bool up = (y < matrix.GetLength(0) - 1) && (matrix[y + 1, x] != 0);
        bool down = (y > 0) && (matrix[y - 1, x] != 0);
        bool left = (x > 0) && (matrix[y, x - 1] != 0);
        bool right = (x < matrix.GetLength(1) - 1) && (matrix[y, x + 1] != 0);

        bool upLeft = (y < matrix.GetLength(0) - 1 && x > 0) && (matrix[y + 1, x - 1] != 0);
        bool upRight = (y < matrix.GetLength(0) - 1 && x < matrix.GetLength(1) - 1) && (matrix[y + 1, x + 1] != 0);
        bool downLeft = (y > 0 && x > 0) && (matrix[y - 1, x - 1] != 0);
        bool downRight = (y > 0 && x < matrix.GetLength(1) - 1) && (matrix[y - 1, x + 1] != 0);

        rotationAngle = 0;

        // Esquinas internas
        if (up && left && !upLeft) // Esquina interna superior izquierda
        {
            rotationAngle = 180;
            return landTileInnerCorner;
        }
        else if (up && right && !upRight) // Esquina interna superior derecha
        {
            rotationAngle = -90;
            return landTileInnerCorner;
        }
        else if (down && left && !downLeft) // Esquina interna inferior izquierda
        {
            rotationAngle = 90;
            return landTileInnerCorner;
        }
        else if (down && right && !downRight) // Esquina interna inferior derecha
        {
            rotationAngle = 0;
            return landTileInnerCorner;
        }
        // Bordes
        else if (up && down && left && !right) // Borde derecho
        {
            rotationAngle = -90;
            return landTileBorder;
        }
        else if (up && down && !left && right) // Borde izquierdo
        {
            rotationAngle = 90;
            return landTileBorder;
        }
        else if (up && !down && left && right) // Borde inferior
        {
            rotationAngle = 0;
            return landTileBorder;
        }
        else if (!up && down && left && right) // Borde superior
        {
            rotationAngle = 180;
            return landTileBorder;
        }
        // Esquinas externas
        else if (!up && down && !left && right) // Esquina externa superior izquierda
        {
            rotationAngle = 180;
            return landTileCorner;
        }
        else if (!up && down && left && !right) // Esquina externa superior derecha
        {
            rotationAngle = -90;
            return landTileCorner;
        }
        else if (up && !down && !left && right) // Esquina externa inferior izquierda
        {
            rotationAngle = 90;
            return landTileCorner;
        }
        else if (up && !down && left && !right) // Esquina externa inferior derecha
        {
            rotationAngle = 0;
            return landTileCorner;
        }
        else
        {
            return landTiles[Random.Range(0, landTiles.Count)];
        }
    }

    private void RotateTile(Tilemap tilemap, Vector3Int position, float angle)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one);
        tilemap.SetTransformMatrix(position, matrix);
    }

    private void SetTileWithRotation(Tilemap tilemap, Vector3Int position, TileBase tile, float angle)
    {
        tilemap.SetTile(position, tile);
        RotateTile(tilemap, position, angle);
    }
}
