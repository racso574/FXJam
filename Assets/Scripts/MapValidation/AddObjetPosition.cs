using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectPosition : MonoBehaviour
{
    private GameObject[] objects;
    private blackboard blackboardScript;

    // Coordenadas límites
    private float minX = 0f;
    private float minY = -575f;
    private float maxX = 575f;
    private float maxY = 0f;

    // Matriz para copiar
    private int[,] copiedMatrix = new int[50, 50];

    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos el array de objetos
        objects = new GameObject[5];

        // Buscamos los objetos por nombre
        for (int i = 0; i < 5; i++)
        {
            objects[i] = GameObject.Find("objeto" + (i + 1));
            if (objects[i] == null)
            {
                Debug.LogError("Object 'objeto" + (i + 1) + "' not found");
            }
        }

        // Obtener la referencia del script blackboard
        blackboardScript = FindObjectOfType<blackboard>();
        if (blackboardScript == null)
        {
            Debug.LogError("Blackboard script not found");
        }
    }

    public int[,] SetObjectPositionInArray()
    {
        // Copiar la matriz del script blackboard
        if (blackboardScript != null)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    copiedMatrix[x, y] = blackboardScript.matrix[x, y];
                }
            }
        }

        // Verificamos si los objetos están dentro de las coordenadas especificadas y guardamos sus posiciones en la matriz
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                // Obtener la posición local del objeto respecto a su padre
                Vector3 localPos = objects[i].transform.localPosition;
                Debug.Log("Object 'objeto" + (i + 1) + "' local position: (" + localPos.x + ", " + localPos.y + ")");

                if (localPos.x >= minX && localPos.x <= maxX && localPos.y >= minY && localPos.y <= maxY)
                {
                    // Calculamos la posición en la matriz
                    int xIndex = Mathf.FloorToInt((localPos.x - minX) / (maxX - minX) * 49);
                    int yIndex = Mathf.FloorToInt((localPos.y - minY) / (maxY - minY) * 49);

                    // Rotamos los índices 90 grados en el sentido de las agujas del reloj
                    int newXIndex = 49 - yIndex;
                    int newYIndex = xIndex;

                    // Nos aseguramos de que los índices estén dentro del rango de la matriz
                    newXIndex = Mathf.Clamp(newXIndex, 0, 49);
                    newYIndex = Mathf.Clamp(newYIndex, 0, 49);

                    // Asignamos el número del objeto a la matriz
                    copiedMatrix[newXIndex, newYIndex] = i + 2; // Asignamos i + 2 para seguir la lógica proporcionada
                    Debug.Log("Object 'objeto" + (i + 1) + "' assigned to matrix position: (" + newXIndex + ", " + newYIndex + ")");
                }
                else
                {
                    Debug.Log("Object 'objeto" + (i + 1) + "' is out of bounds: (" + localPos.x + ", " + localPos.y + ")");
                }
            }
        }

        Debug.Log("Matrix updated and returned");
        PrintMatrix();

        return copiedMatrix;
    }

    private void PrintMatrix()
    {
        string matrixString = "";
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                matrixString += copiedMatrix[i, j] + " ";
            }
            matrixString += "\n";
        }
        Debug.Log(matrixString);
    }
}
