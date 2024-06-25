using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackboard : MonoBehaviour
{
    public GameObject cellPrefab; // Referencia al prefab de la celda
    public Transform gridParent; // Padre de la cuadrícula en la jerarquía
    public float spacing = 2f; // Espaciado entre celdas

    private int[,] matrix = new int[50, 50]; // Matriz privada de enteros de 50x50
    private GameObject[,] cells = new GameObject[50, 50]; // Matriz privada de celdas

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        RectTransform cellRect = cellPrefab.GetComponent<RectTransform>();
        float cellWidth = cellRect.rect.width;
        float cellHeight = cellRect.rect.height;

        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cell.GetComponent<Cell>().Initialize(x, y, this);
                cells[x, y] = cell;

                // Calcular la posición de cada celda
                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(x * (cellWidth + spacing), -y * (cellHeight + spacing));
                }
            }
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
            cells[x, y].GetComponent<Image>().color = Color.white;
            matrix[x, y] = 0;
        }
    }
}

