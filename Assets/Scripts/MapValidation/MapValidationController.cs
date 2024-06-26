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
        int[,] positions = addObjectPosition.SetObjectPositionInArray();
        int[,] randomIslandMatrix = IslandManager.Instance.GetRandomIslandMatrix();

        // Procesar las matrices por separado
        positions = matrixVoidGenerator.ProcessMatrix(positions);
        randomIslandMatrix = matrixVoidGenerator.ProcessMatrix(randomIslandMatrix);

        // Calcular la puntuación de similitud
        float similarityScore = matrixComparator.CompareMatrices(positions, randomIslandMatrix);

        // Ahora puedes usar similarityScore como necesites
        Debug.Log("Similarity Score: " + similarityScore);
    }
}
