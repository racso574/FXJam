using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsComparator : MonoBehaviour
{
    public int CompareMatricesObjects(int[,] array1, int[,] array2)
    {
        int count = 0;
        int rows = array1.GetLength(0);
        int cols = array1.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array1[i, j] >= 2 && array1[i, j] <= 6)
                {
                    if (IsNumberInRadius(array2, array1[i, j], i, j))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private bool IsNumberInRadius(int[,] array, int number, int row, int col)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        int radius = 5;

        for (int i = Mathf.Max(0, row - radius); i <= Mathf.Min(rows - 1, row + radius); i++)
        {
            for (int j = Mathf.Max(0, col - radius); j <= Mathf.Min(cols - 1, col + radius); j++)
            {
                if (array[i, j] == number)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
