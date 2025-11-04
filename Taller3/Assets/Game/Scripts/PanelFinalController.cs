using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelFinalController : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelFinal;           // Panel contenedor
    public TextMeshProUGUI textoTitulo;     // "¡Has completado la misión!"
    public TextMeshProUGUI textoPuntaje;    // Puntaje total
    public TextMeshProUGUI textoItems;      // Ítems recolectados
    public TextMeshProUGUI textoTiempo;     // Tiempo total

    [Header("Botones")]
    public Button botonMenu;                // Volver al menú
    public Button botonSalir;               // Salir del juego

    private bool mostrado = false;

    void Start()
    {
        // Ocultar panel al inicio
        if (panelFinal != null)
            panelFinal.SetActive(false);

        // Asignar eventos a los botones (por seguridad)
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

        // Mensaje de título
        if (textoTitulo != null)
            textoTitulo.text = "¡Has completado la misión!";

        // Mostrar puntaje total
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje total: " + GameManager.instance.Score.ToString();

        // Mostrar ítems recolectados
        if (textoItems != null)
            textoItems.text = "Energías recolectadas: " + GameManager.instance.ItemsCount.ToString();

        // Mostrar tiempo total formateado
        if (textoTiempo != null)
        {
            float tiempo = GameManager.instance.GlobalTime;
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
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
            GameManager.instance.LoadScene("MenuPrincipal"); // Cambia al nombre exacto de tu escena de menú
        }
    }

    // 🔹 Botón: salir del juego
    public void SalirDelJuego()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("Saliendo del juego...");
            GameManager.instance.ExitGame();
        }
    }
}
