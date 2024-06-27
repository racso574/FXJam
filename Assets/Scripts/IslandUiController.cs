using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class IslandUiController : MonoBehaviour
{
    public GameObject panel; // Referencia directa al GameObject del panel
    public TextMeshProUGUI textMeshPro;
    public float fadeOutDuration = 2f;
    public float fadeInDuration = 2f;
    public float textDisplaySpeed = 0.05f;
    public float textFadeOutDelay = 2f;
    public string message;
    public UnityEvent onStartFadeIn; // Evento Unity que será llamado al comenzar el fade in
    public UnityEvent onEndFadeIn; // Evento Unity que será llamado al finalizar el fade in

    private Image panelImage;
    private Color panelColor;
    private Color textColor;

    private void Awake()
    {
        panelImage = panel.GetComponent<Image>();
        panelColor = panelImage.color;
        textColor = textMeshPro.color;

        // Activar el panel y hacer visibles el panel y el texto
        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 1); // Asegúrate de que el panel esté completamente visible
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 0); // Asegúrate de que el texto esté completamente invisible
        panel.SetActive(true);
    }

    private void Start()
    {
        // Hacer fade out al inicio
        StartCoroutine(FadeOutPanel());
    }

    public IEnumerator FadeInPanel()
    {
        float elapsedTime = 0f;

        // Llamar al evento onStartFadeIn al comenzar el fade in
        onStartFadeIn.Invoke();

        // Fade in del panel
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 1);

        // Llamar al evento onEndFadeIn al finalizar el fade in
        onEndFadeIn.Invoke();
    }

    private IEnumerator FadeOutPanel()
    {
        float elapsedTime = 0f;

        // Fade out del panel
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);

        // Mostrar el texto después del fade out del panel
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        // Asegúrate de que el TextMeshProUGUI tenga su tamaño ajustado adecuadamente y overflow configurado
        textMeshPro.enableWordWrapping = true;
        textMeshPro.overflowMode = TMPro.TextOverflowModes.Overflow;

        textMeshPro.text = "";
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 1); // Vuelve a hacer visible el texto

        foreach (char letter in message)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(textDisplaySpeed);
        }

        yield return new WaitForSeconds(textFadeOutDelay); // Tiempo de espera antes de iniciar el fade out del texto
        StartCoroutine(FadeOutText());
    }


    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;

        // Fade out del texto
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 0);
    }
}
