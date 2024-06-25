using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{
   public void GeneratePrefabPositions(int[,] islandMatrix, List<GameObject> prefabsToSpawn)
{
    for (int y = 0; y < islandMatrix.GetLength(0); y++)
    {
        for (int x = 0; x < islandMatrix.GetLength(1); x++)
        {
            int value = islandMatrix[y, x];
            // Solo coloca prefabs si el valor es mayor o igual a 2
            if (value >= 2)
            {
                int prefabIndex = value - 2;
                if (prefabIndex < prefabsToSpawn.Count)
                {
                    Vector3 position = new Vector3(x - islandMatrix.GetLength(1) / 2, (islandMatrix.GetLength(0) - 1 - y) - islandMatrix.GetLength(0) / 2, 0);
                    Instantiate(prefabsToSpawn[prefabIndex], position, Quaternion.identity);
                }
            }
        }
    }
}

}


