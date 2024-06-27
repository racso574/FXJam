using System.Collections;
using UnityEngine;
using TMPro;

public class PuntuationUi : MonoBehaviour
{
    // Referencia al panel que contiene el TextMeshPro
    [SerializeField] private GameObject panel;

    // Referencia al componente TextMeshPro
    [SerializeField] private TextMeshProUGUI puntuacionText;

    [SerializeField] private TextMeshProUGUI puntuacionText2;

    // Variables para guardar las puntuaciones actuales
    private float currentPuntuacion = 0f;
    private int currentObjectScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Asegúrate de que el panel esté desactivado al inicio
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Función para actualizar la puntuación y activar el panel
    public void ActualizarPuntuacion(float puntuacion, int objectscore)
    {
        panel.SetActive(true);
        StartCoroutine(ActualizarPuntuacionGradualmente(puntuacion, objectscore));
    }

    private IEnumerator ActualizarPuntuacionGradualmente(float puntuacionFinal, int objectScoreFinal)
    {
        float duration = 1.0f; // Duración de la animación en segundos
        float elapsed = 0f;

        float initialPuntuacion = currentPuntuacion;
        int initialObjectScore = currentObjectScore;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            currentPuntuacion = Mathf.Lerp(initialPuntuacion, puntuacionFinal, t);
            currentObjectScore = Mathf.RoundToInt(Mathf.Lerp(initialObjectScore, objectScoreFinal, t));

            puntuacionText.text = $"{currentPuntuacion:F1}% Accuracy on Silhouette";
            puntuacionText2.text = $"{currentObjectScore}/3 Objects Correctly Placed";

            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que el valor final se establece al término de la animación
        currentPuntuacion = puntuacionFinal;
        currentObjectScore = objectScoreFinal;

        puntuacionText.text = $"{puntuacionFinal:F1}% Accuracy on Silhouette";
        puntuacionText2.text = $"{objectScoreFinal}/3 Objects Correctly Placed";
    }
}

