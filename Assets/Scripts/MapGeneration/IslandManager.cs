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
    
    private int[,] randomIslandMatrix;



    void Start()
    {
        GenerateRandomIsland();
    }

    public void GenerateRandomIsland()
    {
        randomIslandMatrix = matrixGenerator.GenerateMatrix(seed);
        tilemapBuilder.GenerateIsland(randomIslandMatrix);
        prefabPlacer.GeneratePrefabPositions(randomIslandMatrix, prefabsToSpawn);
        SaveMatrixToPlayerPrefs(randomIslandMatrix, "RandomIslandMatrix");
    }

    private void SaveMatrixToPlayerPrefs(int[,] matrix, string key)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        string matrixString = rows + "," + cols + ";";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrixString += matrix[i, j].ToString() + ",";
            }
        }
        PlayerPrefs.SetString(key, matrixString.TrimEnd(','));
        PlayerPrefs.Save();
    }

  }
