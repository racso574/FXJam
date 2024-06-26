using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    private readonly float targetAspect = 4.0f / 3.0f;

    void Start()
    {
        AdjustAspectRatio();
    }

    void AdjustAspectRatio()
    {
        Camera camera = GetComponent<Camera>();
        if (camera == null) return;

        // Determina la relación de aspecto actual de la pantalla
        float windowAspect = (float)Screen.width / (float)Screen.height;
        // Calcula la escala de la ventana actual
        float scaleHeight = windowAspect / targetAspect;

        // Si la relación de aspecto actual es menor que la relación objetivo, se agregan barras negras horizontales
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else // Si la relación de aspecto actual es mayor o igual a la relación objetivo, se agregan barras negras verticales
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
