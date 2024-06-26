using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IslandUiController : MonoBehaviour
{
    public GameObject panel; // Referencia directa al GameObject del panel
    public TextMeshProUGUI textMeshPro;
    public float fadeOutDuration = 2f;
    public float textDisplaySpeed = 0.05f;
    public float textFadeOutDelay = 2f;
    public string message;

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
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 1); // Asegúrate de que el texto esté completamente visible
        panel.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(FadeOutPanelAndText());
    }

    private IEnumerator FadeOutPanelAndText()
    {
        float elapsedTime = 0f;

        // Fade out del panel y texto al mismo tiempo
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 0);

        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        textMeshPro.text = "";
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 1); // Vuelve a hacer visible el texto

        foreach (char letter in message)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(textDisplaySpeed);
        }

        yield return new WaitForSeconds(textFadeOutDelay); // Tiempo de espera antes de iniciar el segundo fade out
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;

        // Segundo fade out del texto
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
