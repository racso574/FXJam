using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Función para cerrar el juego
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Detener el juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        // No hacer nada en WebGL
        Debug.Log("Exit button pressed, but no action is taken in WebGL.");
#else
        // Cerrar la aplicación en plataformas compiladas
        Application.Quit();
#endif
    }

    // Función para cambiar de escena
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
