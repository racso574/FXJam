using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectPosition : MonoBehaviour
{
     public int[] SetObjectPositionInArray()
    {
        // Lógica para establecer la posición del objeto en el array
        int[] positions = new int[] { 1, 2, 3, 4 }; // Ejemplo de datos
        Debug.Log("Object position set in array");
        return positions;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
