using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public static IslandManager Instance { get; private set; }
    public TilemapBuilder tilemapBuilder;
    public PrefabPlacer prefabPlacer;
    public MatrixGenerator matrixGenerator;
    public int seed;  // Variable pública para la semilla
    public List<GameObject> prefabsToSpawn;  // Nueva lista de prefabs
    
    private int[,] randomIslandMatrix;

    void Awake()
    {
        // Implementar el patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateRandomIsland();
    }

    public void GenerateRandomIsland()
    {
        randomIslandMatrix = matrixGenerator.GenerateMatrix(seed);
        tilemapBuilder.GenerateIsland(randomIslandMatrix);
        prefabPlacer.GeneratePrefabPositions(randomIslandMatrix, prefabsToSpawn);
    }

    public int[,] GetRandomIslandMatrix()
    {
        return randomIslandMatrix;
    }
}
