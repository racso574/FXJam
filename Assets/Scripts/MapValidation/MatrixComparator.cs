using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixComparator : MonoBehaviour
{
    private const int RANGE = 5;
    private const int MAX_POINTS_1 = 5;
    private const int MAX_POINTS_2_TO_6 = 12;

    // Esta función compara dos matrices y devuelve el porcentaje de similitud basado en las reglas descritas
    public float CompareMatrices(int[,] array1, int[,] array2)
    {
        int rows = array1.GetLength(0);
        int cols = array1.GetLength(1);
        int totalPoints = 0;
        int maxPossiblePoints = 0;

        bool[,] matchedPositions = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value1 = array1[i, j];
                if (value1 == 0) continue;

                int value2 = array2[i, j];
                maxPossiblePoints += GetMaxPoints(value1);

                if (value1 == value2 && !matchedPositions[i, j])
                {
                    totalPoints += GetMaxPoints(value1);
                    matchedPositions[i, j] = true;
                }
                else
                {
                    totalPoints += FindNearbyMatch(array2, i, j, value1, matchedPositions);
                }
            }
        }

        return maxPossiblePoints > 0 ? (float)totalPoints / maxPossiblePoints * 100f : 0f;
    }

    private int GetMaxPoints(int value)
    {
        if (value == 1)
        {
            return MAX_POINTS_1;
        }
        else if (value >= 2 && value <= 6)
        {
            return MAX_POINTS_2_TO_6;
        }
        return 0;
    }

    private int FindNearbyMatch(int[,] array, int row, int col, int targetValue, bool[,] matchedPositions)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
        queue.Enqueue((row, col, 0));

        while (queue.Count > 0)
        {
            var (currentRow, currentCol, distance) = queue.Dequeue();

            if (distance > RANGE) break;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = currentRow + i;
                    int newCol = currentCol + j;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                        !matchedPositions[newRow, newCol] && array[newRow, newCol] == targetValue)
                    {
                        matchedPositions[newRow, newCol] = true;
                        int points = Mathf.Max(GetMaxPoints(targetValue) - distance, 1);
                        return points;
                    }

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                        !matchedPositions[newRow, newCol])
                    {
                        queue.Enqueue((newRow, newCol, distance + 1));
                    }
                }
            }
        }

        return 0;
    }
}
