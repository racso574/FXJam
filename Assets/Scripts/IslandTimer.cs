using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class IslandTimer : MonoBehaviour
{
    public GameObject panel;             // Panel de UI
    public TextMeshProUGUI textMeshPro;  // TextMeshPro UI
    public Button button;
    public Button button2;  // Botón de UI
    public float waitTime = 10.0f;       // Tiempo de espera configurable
    public UnityEvent onCanvasVisible;   // Evento de Unity

    private float timer = 0.0f;
    private bool fadeInStarted = false;

    // Awake is called when el script instance is being loaded
    void Awake()
    {
        // Desactiva los componentes
        panel.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);    
    }

    // Start is called before the first frame update
    void Start()
    {
        // No es necesario hacer nada aquí en este caso
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime && !fadeInStarted)
        {
            // Establece la opacidad en 0 antes de activarlos
            SetUIVisibility(0.0f);

            // Activa los componentes antes de iniciar el fade-in
            panel.SetActive(true);
            textMeshPro.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);

            StartCoroutine(FadeInUI());
            fadeInStarted = true;
        }
    }

    IEnumerator FadeInUI()
    {
        float duration = 1.0f; // Duración del fade-in
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            SetUIVisibility(alpha);
            yield return null;
        }

        SetUIVisibility(1.0f); // Asegúrate de que la UI esté completamente visible al final
        onCanvasVisible?.Invoke(); // Invoca el evento cuando el canvas sea visible
    }

    void SetUIVisibility(float alpha)
    {
        CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = panel.AddComponent<CanvasGroup>();
        }

        panelCanvasGroup.alpha = alpha;

        CanvasGroup textMeshProCanvasGroup = textMeshPro.GetComponent<CanvasGroup>();
        if (textMeshProCanvasGroup == null)
        {
            textMeshProCanvasGroup = textMeshPro.gameObject.AddComponent<CanvasGroup>();
        }

        textMeshProCanvasGroup.alpha = alpha;

        CanvasGroup buttonCanvasGroup = button.GetComponent<CanvasGroup>();
        if (buttonCanvasGroup == null)
        {
            buttonCanvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }

        buttonCanvasGroup.alpha = alpha;

        CanvasGroup buttonCanvasGroup2 = button2.GetComponent<CanvasGroup>();
        if (buttonCanvasGroup == null)
        {
            buttonCanvasGroup2 = button2.gameObject.AddComponent<CanvasGroup>();
        }

        buttonCanvasGroup2.alpha = alpha;
    }
}
