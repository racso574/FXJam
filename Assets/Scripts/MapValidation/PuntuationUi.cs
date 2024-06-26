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
    public void ActualizarPuntuacion(float puntuacion)
    {
        // Activar el panel
        panel.SetActive(true);
        
        // Actualizar el texto del TextMeshPro con la nueva puntuación
        puntuacionText.text = puntuacion.ToString();
    }
}
