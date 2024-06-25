using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapValidationController : MonoBehaviour
{
    // Referencia al script AddObjectPosition
    public AddObjectPosition addObjectPosition;

    // Esta función será llamada para iniciar la validación
    public void StartValidation()
    {
        int[] positions = addObjectPosition.SetObjectPositionInArray();
    }
}
