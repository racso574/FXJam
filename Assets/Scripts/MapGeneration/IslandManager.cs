using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public TilemapBuilder tilemapBuilder;
    public PrefabPlacer prefabPlacer;
    public MatrixGenerator matrixGenerator;
    public int seed;  // Variable pública para la semilla
    public int matrixSize; // Definir el tamaño de la matriz
    public int floorCount; // Definir el número de pisos
    int objectCount; // Definir el número de objetos, aunque no se use
    public List<GameObject> prefabsToSpawn;  // Nueva lista de prefabs
    

    void Start()
    {
        GenerateRandomIsland();
    }

    public void GenerateRandomIsland()
    {
        //if (seed == 0)
        //{
            seed = Random.Range(int.MinValue, int.MaxValue);
        //}

        int[,] randomIslandMatrix = matrixGenerator.GenerateMatrix(matrixSize, floorCount, objectCount, seed);

        tilemapBuilder.GenerateIsland(randomIslandMatrix);
        prefabPlacer.GeneratePrefabPositions(randomIslandMatrix, prefabsToSpawn);
    }
}
