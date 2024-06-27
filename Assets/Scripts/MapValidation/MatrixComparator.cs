using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixComparator : MonoBehaviour
{
    public float CompareMatrices(int[,] array1, int[,] array2)
    {
        int rows = array1.GetLength(0);
        int cols = array1.GetLength(1);

        if (rows != array2.GetLength(0) || cols != array2.GetLength(1))
        {
            Debug.LogError("Matrices must have the same dimensions.");
            return 0f;
        }

        int totalOnes1 = 0;
        int matchedOnes = 0;
        int totalOnes2 = 0;

        // Matriz para marcar las casillas de array2 que ya han sido consideradas
        bool[,] checkedArray2 = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array1[i, j] == 1)
                {
                    totalOnes1++;
                    if (IsPixelMatch(array1, array2, i, j, checkedArray2))
                    {
                        matchedOnes++;
                    }
                }
                if (array2[i, j] == 1)
                {
                    totalOnes2++;
                }
            }
        }

        if (totalOnes1 == 0) return 0f; // Evitar división por cero

        // Calcular el porcentaje de similitud
        float similarity = (float)matchedOnes / totalOnes1 * 100f;

        // Calcular la penalización por los 1s extra en array2
        int extraOnes = totalOnes2 - totalOnes1;
        if (extraOnes > 0)
        {
            float extraPenalty = (float)extraOnes / totalOnes1 * 100f;
            float finalPercentage = similarity - extraPenalty;
            return finalPercentage < 0 ? 0 : finalPercentage;
        }

        return similarity;
    }

    private bool IsPixelMatch(int[,] array1, int[,] array2, int x, int y, bool[,] checkedArray2)
    {
        if (array2[x, y] == 1)
        {
            checkedArray2[x, y] = true;
            return true;
        }

        bool up = x > 0 && array1[x - 1, y] == 1;
        bool down = x < array1.GetLength(0) - 1 && array1[x + 1, y] == 1;
        bool left = y > 0 && array1[x, y - 1] == 1;
        bool right = y < array1.GetLength(1) - 1 && array1[x, y + 1] == 1;

        int searchRadius = 4;

        if (!up && SearchInRadius(array2, x, y, -1, 0, searchRadius, checkedArray2)) return true;
        if (!down && SearchInRadius(array2, x, y, 1, 0, searchRadius, checkedArray2)) return true;
        if (!left && SearchInRadius(array2, x, y, 0, -1, searchRadius, checkedArray2)) return true;
        if (!right && SearchInRadius(array2, x, y, 0, 1, searchRadius, checkedArray2)) return true;

        return false;
    }

    private bool SearchInRadius(int[,] array, int x, int y, int dx, int dy, int radius, bool[,] checkedArray2)
    {
        for (int i = 1; i <= radius; i++)
        {
            int newX = x + dx * i;
            int newY = y + dy * i;

            if (newX >= 0 && newX < array.GetLength(0) && newY >= 0 && newY < array.GetLength(1))
            {
                if (array[newX, newY] == 1)
                {
                    checkedArray2[newX, newY] = true;
                    return true;
                }
            }
        }

        return false;
    }
}
