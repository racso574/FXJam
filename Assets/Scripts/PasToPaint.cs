using UnityEngine;

public class PasToPaint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Encuentra el objeto Canvasuiisland en la escena
            GameObject canvas = GameObject.Find("Canvasuiisland");
            if (canvas != null)
            {
                // Obt�n el componente IslandUiController del objeto Canvasuiisland
                IslandUiController uiController = canvas.GetComponent<IslandUiController>();
                if (uiController != null)
                {
                    // Ejecuta el m�todo FadeInPanel del IslandUiController
                    uiController.StartCoroutine(uiController.FadeInPanel());
                }
                else
                {
                    Debug.LogError("No se encontr� el componente IslandUiController en Canvasuiisland.");
                }
            }
            else
            {
                Debug.LogError("No se encontr� el objeto Canvasuiisland en la escena.");
            }
        }
    }
}

