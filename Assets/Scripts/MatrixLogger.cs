using UnityEngine;

public class MatrixLogger : MonoBehaviour
{
    int[,] matrix = new int[50, 50]; // Define una matriz de 50x50

    void Start()
    {
        // Inicialización de la matriz con algunos valores para verificar la salida
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                matrix[i, j] = i * 50 + j + 1; // Solo para llenar con valores secuenciales
            }
        }

        LogMatrix();
    }

    void LogMatrix()
    {
        string matrixString = "";
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                matrixString += matrix[i, j] + " ";
            }
            matrixString += "\n";
        }
        Debug.Log(matrixString);
    }
}

