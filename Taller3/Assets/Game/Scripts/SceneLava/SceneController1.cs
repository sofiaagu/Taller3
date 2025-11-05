using UnityEngine;
using TMPro;

public class SceneController1 : MonoBehaviour
{
    [Header("Referencias Generales")]
    public GameObject portalVisual; // El modelo o efecto del portal
    public string nombreEscenaDestino = "Hielo";

    [Header("Referencias de carga")]
    private LoaderScene sceneLoader;

    [Header("Efectos opcionales")]
    public AudioClip sonidoTeletransporte;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoColisiones;
    public TextMeshProUGUI textoItems; //  aquí se mostrarán las bolas de fuego

    private void Start()
    {
        sceneLoader = FindAnyObjectByType<LoaderScene>();

        if (portalVisual == null)
            portalVisual = gameObject;

        if (sceneLoader == null)
            Debug.LogWarning("No se encontró ningún LoaderScene en la escena.");

        if (GameManager.Instance == null)
            Debug.LogWarning(" No hay un GameManager activo (asegúrate de venir desde el menú).");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" Jugador entró al portal.");

            if (sonidoTeletransporte != null)
                AudioSource.PlayClipAtPoint(sonidoTeletransporte, transform.position);

            if (sceneLoader != null)
                sceneLoader.LoaderScenes(nombreEscenaDestino);
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        GameManager gm = GameManager.Instance;

        // Actualizar la UI con los datos del GameManager
        if (textoScore != null)
            textoScore.text = $"{gm.score}";

        if (textoColisiones != null)
            textoColisiones.text = $"{gm.colisionesTotales}";

        if (textoItems != null)
            textoItems.text = $"{gm.itemsRecogidos}";

        // 🔹 Lógica del portal (sin cambios)
        bool mostrar = gm.colisionesTotales < 3 || gm.score > 80;
        portalVisual.SetActive(mostrar);
    }
}
