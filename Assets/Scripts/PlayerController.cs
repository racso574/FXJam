using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float accelerationTime = 0.2f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 movementVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject targetObject = GameObject.Find("Objeto1(Clone)");
        if (targetObject != null)
        {
            // Obtener la posición del objeto encontrado
            Vector3 targetPosition = targetObject.transform.position;

            // Ajustar la posición Y con un desplazamiento de +2
            targetPosition.y += 1;

            // Teletransportar el jugador a la nueva posición
            transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("Objeto1(Clone) no encontrado en la escena.");
        }
    }

    private void Update()
    {
        // Captura las entradas de movimiento
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();

        // Voltear el sprite según la dirección de movimiento
        if (movementInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Rotar ligeramente el sprite mientras se mueve
        if (movementInput != Vector2.zero)
        {
            float tiltAngle = Mathf.Sin(Time.time * 10) * -2f;
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
        else
        {
            // Resetear la rotación cuando está quieto
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        // Calcula y aplica la velocidad del movimiento con suavizado
        Vector2 targetVelocity = movementInput * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref movementVelocity, accelerationTime);
    }
}
