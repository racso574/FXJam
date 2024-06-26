using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixVoidGenerator : MonoBehaviour
{


    // Función que recibe una matriz de enteros y devuelve otra matriz de enteros
    public int[,] ProcessMatrix(int[,] inputMatrix)
    {
        int rows = inputMatrix.GetLength(0);
        int cols = inputMatrix.GetLength(1);
        int[,] outputMatrix = (int[,])inputMatrix.Clone();

        // Direcciones para los vecinos: arriba, abajo, izquierda, derecha y diagonales
        int[,] directions = new int[,]
        {
            {-1, -1}, {-1, 0}, {-1, 1},
            { 0, -1},         { 0, 1},
            { 1, -1}, { 1, 0}, { 1, 1}
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (inputMatrix[i, j] == 1)
                {
                    bool hasZeroNeighbor = false;
                    for (int d = 0; d < directions.GetLength(0); d++)
                    {
                        int newRow = i + directions[d, 0];
                        int newCol = j + directions[d, 1];

                        if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && inputMatrix[newRow, newCol] == 0)
                        {
                            hasZeroNeighbor = true;
                            break;
                        }
                    }

                    if (!hasZeroNeighbor)
                    {
                        outputMatrix[i, j] = 0;
                    }
                }
            }
        }

        return outputMatrix;
    }

    
        
    
}
