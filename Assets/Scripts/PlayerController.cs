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
    }

    private void Update()
    {
        // Captura las entradas de movimiento
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();
    }

    private void FixedUpdate()
    {
        // Calcula y aplica la velocidad del movimiento con suavizado
        Vector2 targetVelocity = movementInput * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref movementVelocity, accelerationTime);
    }
}

