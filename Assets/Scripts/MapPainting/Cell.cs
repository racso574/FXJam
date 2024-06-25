using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Image image;
    public bool isBlack = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) // Clic izquierdo
        {
            SetColor(true);
        }
        else if (Input.GetMouseButton(1)) // Clic derecho
        {
            SetColor(false);
        }
    }

    void SetColor(bool black)
    {
        isBlack = black;
        image.color = isBlack ? Color.black : Color.white;
    }
}
