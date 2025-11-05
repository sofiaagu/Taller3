using UnityEngine;

public class SceneController1 : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject portalVisual; // el modelo o efecto del portal
    public GameManager gameManager; // referencia al GameManager
    public string nombreEscenaDestino = "Hielo";

    private LoaderScene sceneLoader;

    private void Start()

    {
        sceneLoader = FindAnyObjectByType<LoaderScene>();

        if (portalVisual == null)
            portalVisual = gameObject; // usa el propio objeto si no se asigna

        if (sceneLoader == null)
        {
            Debug.LogWarning("No se encontró ningún LoaderScene en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador entró al portal.");

            // Solo cambia de escena si el loader existe
            if (sceneLoader != null)
            {
                sceneLoader.LoaderScenes(nombreEscenaDestino);
            }
        }
    }

    private void Update()
    {
        if (gameManager == null)
            return;

        // Condición: se muestra si tiene menos de 3 colisiones o más de 80 puntos
        bool mostrar = gameManager.colisionesTotales < 3 || gameManager.score > 80;

        portalVisual.SetActive(mostrar);
    }
}