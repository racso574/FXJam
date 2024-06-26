using System.Collections;
using UnityEngine;
using TMPro;

public class TextoParpadeoFade : MonoBehaviour
{
    public TextMeshProUGUI texto; // Asigna el TextMeshPro en el inspector
    public float duracionParpadeo = 0.5f; // Duraci�n de un parpadeo (en segundos)
    public int numeroDeParpadeos = 5; // N�mero de veces que parpadear�

    private void Start()
    {
        if (texto == null)
        {
            texto = GetComponent<TextMeshProUGUI>();
        }
        StartCoroutine(Parpadeo());
    }

    private IEnumerator Parpadeo()
    {
        for (int i = 0; i < numeroDeParpadeos; i++)
        {
            // Plena opacidad
            texto.alpha = 1f;
            yield return new WaitForSeconds(duracionParpadeo);

            // Transparente
            texto.alpha = 0f;
            yield return new WaitForSeconds(duracionParpadeo);
        }

        // La �ltima vez se queda transparente y se desactiva
        texto.alpha = 0f;
        gameObject.SetActive(false);
    }
}
