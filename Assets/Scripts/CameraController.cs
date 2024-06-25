using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador

    private void Start()
    {
        // Encuentra al jugador por su nombre
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player no encontrado. Asegúrate de que el objeto del jugador se llame 'Player'.");
        }
    }

    private void LateUpdate()
    {
        // Asegúrate de que la cámara siga al jugador
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z; // Mantén la posición z de la cámara
            transform.position = newPosition;
        }
    }
}
