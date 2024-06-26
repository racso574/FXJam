using UnityEngine;

public class MatrixComparator : MonoBehaviour
{
    // Esta función compara dos matrices y devuelve el porcentaje de similitud usando el índice de Jaccard
    public float CompareMatrices(int[,] array1, int[,] array2)
    {
        int rows = array1.GetLength(0);
        int cols = array1.GetLength(1);

        int intersection = 0;
        int union = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array1[i, j] == 1 || array2[i, j] == 1)
                {
                    union++;
                }
                if (array1[i, j] == 1 && array2[i, j] == 1)
                {
                    intersection++;
                }
            }
        }

        return union > 0 ? (float)intersection / union * 100f : 0f;
    }

 
}