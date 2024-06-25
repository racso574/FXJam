using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Ejemplo de uso
        int[,] matrix = GenerateMatrix(5, 10, 0, 42);
        PrintMatrix(matrix);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] GenerateMatrix(int size, int floorCount, int objectCount, int seed)
    {
        System.Random random = new System.Random(seed);
        int[,] matrix = new int[size, size];

        // Inicializamos la matriz con ceros
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = 0;
            }
        }

        // Colocamos un 1 en el centro
        int centerX = size / 2;
        int centerY = size / 2;
        matrix[centerX, centerY] = 1;

        // Rellenamos la matriz con el floorCount adicional
        List<Vector2Int> positions = new List<Vector2Int> { new Vector2Int(centerX, centerY) };
        int count = 1;

        while (count < floorCount)
        {
            Vector2Int currentPos = positions[random.Next(positions.Count)];
            List<Vector2Int> neighbors = GetNeighbors(currentPos, size);

            foreach (Vector2Int neighbor in neighbors)
            {
                if (matrix[neighbor.x, neighbor.y] == 0)
                {
                    matrix[neighbor.x, neighbor.y] = 1;
                    positions.Add(neighbor);
                    count++;
                    break;
                }
            }
        }

        return matrix;
    }

    List<Vector2Int> GetNeighbors(Vector2Int position, int size)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (position.x > 0) neighbors.Add(new Vector2Int(position.x - 1, position.y));
        if (position.x < size - 1) neighbors.Add(new Vector2Int(position.x + 1, position.y));
        if (position.y > 0) neighbors.Add(new Vector2Int(position.x, position.y - 1));
        if (position.y < size - 1) neighbors.Add(new Vector2Int(position.x, position.y + 1));

        return neighbors;
    }

    void PrintMatrix(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        string matrixString = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrixString += matrix[i, j] + " ";
            }
            matrixString += "\n";
        }
        Debug.Log(matrixString);
    }
}

