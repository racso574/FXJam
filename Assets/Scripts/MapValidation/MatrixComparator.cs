using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixComparator : MonoBehaviour
{
    // Esta función compara dos matrices y devuelve el porcentaje de similitud basado en las reglas descritas
    public float CompareMatrices(int[,] array1, int[,] array2)
    {
        int rows = array1.GetLength(0);
        int cols = array1.GetLength(1);
        int totalPoints = 0;
        int maxPossiblePoints = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value1 = array1[i, j];
                if (value1 == 0) continue; // Los ceros no cuentan para la precisión

                int value2 = array2[i, j];

                if (value1 == 1)
                {
                    maxPossiblePoints += 5;
                    if (value1 == value2)
                    {
                        totalPoints += 5;
                    }
                    else
                    {
                        totalPoints += FindNearbyMatch(array2, i, j, value1, 5, 5);
                    }
                }
                else if (value1 >= 2 && value1 <= 6)
                {
                    maxPossiblePoints += 12;
                    if (value1 == value2)
                    {
                        totalPoints += 12;
                    }
                    else
                    {
                        totalPoints += FindNearbyMatch(array2, i, j, value1, 6, 12);
                    }
                }
            }
        }

        return maxPossiblePoints > 0 ? (float)totalPoints / maxPossiblePoints * 100f : 0f;
    }

    // Esta función busca un valor específico en una matriz dentro de un rango dado y devuelve puntos basado en la proximidad
    private int FindNearbyMatch(int[,] array, int row, int col, int targetValue, int range, int maxPoints)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int r = 1; r <= range; r++)
        {
            int points = maxPoints - r + 1;
            for (int i = -r; i <= r; i++)
            {
                for (int j = -r; j <= r; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && array[newRow, newCol] == targetValue)
                    {
                        return points;
                    }
                }
            }
        }
        return 0; // No se encontró coincidencia en el rango dado
    }
}
