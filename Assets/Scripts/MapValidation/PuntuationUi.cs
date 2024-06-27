using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuationUi : MonoBehaviour
{
    // Referencia al panel que contiene el TextMeshPro
    [SerializeField] private GameObject panel;
    
    // Referencia al componente TextMeshPro
    [SerializeField] private TextMeshProUGUI puntuacionText;

    [SerializeField] private TextMeshProUGUI puntuacionText2;

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
        // Activar el panel
        panel.SetActive(true);

        puntuacionText.text = $"{puntuacion:F1}% Accuracy on Silhouette";
        puntuacionText2.text = $"{objectscore}/3 Objects Correctly Placed";
    }
}
