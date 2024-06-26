using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public TilemapBuilder tilemapBuilder;
    public PrefabPlacer prefabPlacer;
    public MatrixGenerator matrixGenerator;
    public int seed;  // Variable pública para la semilla
    public List<GameObject> prefabsToSpawn;  // Nueva lista de prefabs
    

    void Start()
    {
        GenerateRandomIsland();
    }

    public void GenerateRandomIsland()
    {
        int[,] randomIslandMatrix = matrixGenerator.GenerateMatrix(seed);

        tilemapBuilder.GenerateIsland(randomIslandMatrix);
        prefabPlacer.GeneratePrefabPositions(randomIslandMatrix, prefabsToSpawn);
    }
}
