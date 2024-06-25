using UnityEngine;
using UnityEngine.EventSystems;

public class StickerMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 initialPosition;
    private int initialSiblingIndex;
    private Vector3 initialScale;
    private Vector2 initialPivot;

    [SerializeField]
    private GameObject panel;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        // Guardar la posición inicial del objeto
        initialPosition = rectTransform.anchoredPosition;
        // Guardar el índice de orden inicial del objeto en la jerarquía
        initialSiblingIndex = rectTransform.GetSiblingIndex();
        // Guardar la escala inicial del objeto
        initialScale = rectTransform.localScale;
        // Guardar el pivote inicial del objeto
        initialPivot = rectTransform.pivot;

        // Asegurarse de que el panel esté desactivado inicialmente
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        // Mover este objeto al final de la jerarquía de sus hermanos (lo pone arriba)
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount - 1);
        // Cambiar la escala del objeto
        rectTransform.localScale = new Vector3(0.3f, 0.3f, 1f);
        // Cambiar el pivote del objeto al centro
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        // Mover el objeto para que el ratón esté en el centro
        rectTransform.position = Input.mousePosition;
        // Activar el panel
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        // Comprobar si la nueva posición está dentro de los límites
        if (!IsWithinBounds(rectTransform.anchoredPosition))
        {
            // Volver a la posición inicial si está fuera de los límites
            rectTransform.anchoredPosition = initialPosition;
            // Restaurar el índice de orden inicial en la jerarquía
            rectTransform.SetSiblingIndex(initialSiblingIndex);
            // Restaurar la escala inicial del objeto
            rectTransform.localScale = initialScale;
            // Restaurar el pivote inicial del objeto
            rectTransform.pivot = initialPivot;
        }
        // Desactivar el panel
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    private bool IsWithinBounds(Vector2 position)
    {
        // Definir los límites
        float minX = 0f;
        float minY = -575f;
        float maxX = 575f;
        float maxY = 0f;

        // Comprobar si la posición está dentro de los límites
        if (position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY)
        {
            return true;
        }

        return false;
    }
}
