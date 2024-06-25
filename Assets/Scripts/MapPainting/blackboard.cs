using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class blackboard : MonoBehaviour
{

    public GameObject cellPrefab; // Referencia al prefab de la celda
    public Transform gridParent; // Padre de la cuadrícula en la jerarquía

    private int[,] matrix = new int[50, 50]; // Matriz privada de enteros de 50x50
    private GameObject[,] cells = new GameObject[50, 50]; // Matriz privada de celdas
    
}
