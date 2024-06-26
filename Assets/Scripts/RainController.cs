using UnityEngine;

public class RainController : MonoBehaviour
{
    public ParticleSystem rainParticles; // Referencia al sistema de partículas
    public float firstThreshold = 5.0f; // Tiempo para el primer punto
    public float secondThreshold = 10.0f; // Tiempo para el segundo punto
    public float thirdThreshold = 15.0f; // Tiempo para el tercer punto

    private float timer;

    void Awake()
    {
        // Desactivar las partículas al iniciar
        if (rainParticles != null)
        {
            var emission = rainParticles.emission;
            emission.enabled = false;
        }
    }

    void Update()
    {
        // Incrementar el temporizador
        timer += Time.deltaTime;

        if (timer >= firstThreshold && timer < secondThreshold)
        {
            // Activar las partículas
            if (rainParticles != null)
            {
                var emission = rainParticles.emission;
                emission.enabled = true;
            }
        }
        else if (timer >= secondThreshold && timer < thirdThreshold)
        {
            // Modificar el comportamiento de las partículas en el segundo punto
            if (rainParticles != null)
            {
                var main = rainParticles.main;
                main.simulationSpeed = 2.5f;

                var emission = rainParticles.emission;
                emission.rateOverTime = 100.0f;
            }
        }
        else if (timer >= thirdThreshold)
        {
            // Modificar el comportamiento de las partículas en el tercer punto
            if (rainParticles != null)
            {
                var main = rainParticles.main;
                main.simulationSpeed = 3.4f;

                var emission = rainParticles.emission;
                emission.rateOverTime = 200.0f;
            }
        }
    }
}
