using UnityEngine;
using UnityEngine.SceneManagement;

public class PasToPaint : MonoBehaviour
{
    // Este método se llama cuando otro collider 2D entra en el trigger 2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Cambia a la escena con el índice 2
            SceneManager.LoadScene(2);
        }
    }
}

