using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelFinalController : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelFinal;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoItems;
    public TextMeshProUGUI textoTiempo;

    [Header("Botones")]
    public Button botonMenu;
    public Button botonSalir;

    private bool mostrado = false;

    void Start()
    {
        // Ocultar panel al inicio
        if (panelFinal != null)
            panelFinal.SetActive(false);

        // Asignar eventos a los botones
        if (botonMenu != null)
            botonMenu.onClick.AddListener(VolverAlMenu);

        if (botonSalir != null)
            botonSalir.onClick.AddListener(SalirDelJuego);
    }

    // 🔹 Mostrar el panel con los resultados finales
    public void MostrarPanelFinal()
    {
        if (mostrado || panelFinal == null) return;
        mostrado = true;

        panelFinal.SetActive(true);

        // Título
        if (textoTitulo != null)
            textoTitulo.text = "¡GANASTE!";

        // Puntaje total
        if (textoPuntaje != null)
            textoPuntaje.text = "Score total: " + GameManager.Instance.score.ToString();

        // Ítems recolectados (si tu GameManager no tiene ItemsCount, puedes quitar esto)
        if (textoItems != null)
        {
            // Si no existe ItemsCount, mostramos las colisiones como alternativa
            string items = GameManager.Instance.colisionesTotales.ToString();
            textoItems.text = "Items recolectados: " + items;
        }

        // Tiempo total (si no manejas tiempo, muestra 00:00)
        if (textoTiempo != null)
        {
            float tiempo = 0;
            if (GameManager.Instance != null && GameManager.Instance is not null)
                tiempo = GameManager.Instance.score; // o cualquier variable que manejes como tiempo

            int minutos = (int)(tiempo / 60);
            int segundos = (int)(tiempo % 60);
            int milisegundos = (int)((tiempo - (int)tiempo) * 100);
            textoTiempo.text = $"Tiempo total: {minutos:00}:{segundos:00}:{milisegundos:00}";
        }

        Debug.Log("Panel final mostrado. Juego completado.");
    }

    // 🔹 Botón: volver al menú
    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        SceneManager.LoadScene("MenuPrincipal"); // 🔸 Cambia este nombre si tu menú se llama distinto
    }

    // 🔹 Botón: salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
