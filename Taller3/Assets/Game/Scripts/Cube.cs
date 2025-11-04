using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Configuración de colisión")]
    public float tiempoEntreColisiones = 1.0f; 
    private float ultimaColision = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // evita múltiples detecciones seguidas
            if (Time.time - ultimaColision >= tiempoEntreColisiones)
            {
                ultimaColision = Time.time;

                // llama al contador global
                if (GameManager.Instance != null)
                    GameManager.Instance.RegistrarColision();
            }
        }
    }
}
