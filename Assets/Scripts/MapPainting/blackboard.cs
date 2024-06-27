using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackboard : MonoBehaviour
{
    public GameObject cellPrefab; // Referencia al prefab de la celda
    public Transform gridParent; // Padre de la cuadrícula en la jerarquía
    public float spacing = 0f; // Espaciado entre celdas

    public int[,] matrix = new int[50, 50]; // Matriz privada de enteros de 50x50
    private GameObject[,] cells = new GameObject[50, 50]; // Matriz privada de celdas

    public float cellsPerSecond = 10f; // Número de celdas a dibujar por segundo

    Color lightGray = new Color32(214, 214, 214, 255);


    void Start()
    {
        InitializeGrid();
        
    }

    void InitializeGrid()
    {
        RectTransform cellRect = cellPrefab.GetComponent<RectTransform>();
        float cellWidth = cellRect.rect.width;
        float cellHeight = cellRect.rect.height;

        // Iterar sobre columnas primero
        for (int x = 0; x < 50; x++)
        {
            // Iterar sobre filas después
            for (int y = 0; y < 50; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cell.GetComponent<Cell>().Initialize(y, x, this);
                cells[y, x] = cell;

                // Calcular la posición de cada celda
                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    // Calcular la posición de la celda con el espaciado adecuado
                    float posX = x * spacing;
                    float posY = -y * spacing; // Nota: el signo negativo en Y es para alinear en un sistema de coordenadas de GUI
                    rectTransform.anchoredPosition = new Vector2(posX, posY);
                }
            }
        }

        // Ajustar el tamaño del gridParent para que se ajuste al tamaño de la cuadrícula
        RectTransform gridRectTransform = gridParent.GetComponent<RectTransform>();
        if (gridRectTransform != null)
        {
            gridRectTransform.sizeDelta = new Vector2(
                50 * (cellWidth + spacing) - spacing,
                50 * (cellHeight + spacing) - spacing
            );
        }
    }

    public void UpdateCell(int x, int y, bool isLeftClick)
    {
        
        if (isLeftClick)
        {
            cells[x, y].GetComponent<Image>().color = Color.black;
            matrix[x, y] = 1;
        }
        else
        {
            cells[x, y].GetComponent<Image>().color = lightGray;
            matrix[x, y] = 0;
        }

        // Mostrar el estado de la matriz en el log
        LogMatrix();
    }

    public Coroutine SetMatrix(int[,] newMatrix)
    {
        // Recolectar todas las posiciones de 1 en la nueva matriz
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int i = 0; i < newMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < newMatrix.GetLength(1); j++)
            {
                if (newMatrix[i, j] == 1)
                {
                    positions.Add(new Vector2Int(i, j));
                }
            }
        }

        // Iniciar la corrutina para pintar las celdas y retornarla
        return StartCoroutine(DrawCells(positions));
    }


    private IEnumerator DrawCells(List<Vector2Int> positions)
    {
        float delay = 1f / cellsPerSecond;

        foreach (var pos in positions)
        {
            int i = pos.x;
            int j = pos.y;

            // Cambiar a verde si la celda está pintada de negro y no se va a pintar de rojo
            if (cells[i, j].GetComponent<Image>().color == Color.black)
            {
                cells[i, j].GetComponent<Image>().color = Color.green;
            }
            // Pintar de rojo si la celda no está pintada de negro
            else if (cells[i, j].GetComponent<Image>().color != Color.black)
            {
                cells[i, j].GetComponent<Image>().color = Color.red;
                matrix[i, j] = 1;
            }

            yield return new WaitForSeconds(delay);
        }
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
