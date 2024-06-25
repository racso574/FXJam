using UnityEngine;

public class StickerMover : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    void Start()
    {
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider.OverlapPoint(mousePos))
            {
                isDragging = true;
                offset = transform.position - (Vector3)mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0)) // Soltar clic izquierdo
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            transform.position = newPos + offset;
        }
    }
}
