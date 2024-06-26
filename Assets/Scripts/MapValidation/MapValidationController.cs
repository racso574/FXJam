using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapValidationController : MonoBehaviour
{
    // Referencia al script AddObjectPosition
    public AddObjectPosition addObjectPosition;

    // Referencia al script MatrixVoidGenerator
    private MatrixVoidGenerator matrixVoidGenerator;

    // Referencia al script MatrixComparator
    private MatrixComparator matrixComparator;

    // Referencia al script PuntuationUi
    public PuntuationUi puntuacionUi;

    void Start()
    {
        // Obtener la referencia al MatrixVoidGenerator
        matrixVoidGenerator = GetComponent<MatrixVoidGenerator>();

        // Obtener la referencia al MatrixComparator
        matrixComparator = GetComponent<MatrixComparator>();
    }

    // Esta función será llamada para iniciar la validación
    public void StartValidation()
    {
        // Cargar las matrices desde sus respectivas fuentes
        int[,] positions = addObjectPosition.SetObjectPositionInArray();
        int[,] randomIslandMatrix = LoadMatrixFromPlayerPrefs("RandomIslandMatrix");

        if (randomIslandMatrix == null)
        {
            Debug.LogError("No island matrix found in PlayerPrefs.");
            return;
        }

        // Procesar las matrices por separado
        positions = matrixVoidGenerator.ProcessMatrix(positions);
        randomIslandMatrix = matrixVoidGenerator.ProcessMatrix(randomIslandMatrix);

        // Calcular la puntuación de similitud
        float similarityScore = matrixComparator.CompareMatrices(randomIslandMatrix, positions);

        // Pasar la puntuación de similitud a PuntuationUi
        puntuacionUi.ActualizarPuntuacion(similarityScore);

        // Ahora puedes usar similarityScore como necesites
        Debug.Log("Similarity Score: " + similarityScore);
    }

    private int[,] LoadMatrixFromPlayerPrefs(string key)
    {
        string matrixString = PlayerPrefs.GetString(key);
        if (string.IsNullOrEmpty(matrixString))
        {
            return null; // O manejar el caso donde la clave no existe
        }

        string[] parts = matrixString.Split(';');
        string[] dimensions = parts[0].Split(',');
        int rows = int.Parse(dimensions[0]);
        int cols = int.Parse(dimensions[1]);

        int[,] matrix = new int[rows, cols];
        string[] values = parts[1].Split(',');

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = int.Parse(values[i * cols + j]);
            }
        }

        return matrix;
    }
}
