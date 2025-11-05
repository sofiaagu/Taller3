using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControllerScene2 : MonoBehaviour
{
    [Header("📊 Referencias UI")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoItems;

    [Header("⏱️ Referencias de tiempo (desde PanelTime)")]
    public TextMeshProUGUI minTMP;
    public TextMeshProUGUI segTMP;
    public TextMeshProUGUI milSegTMP;

    [Header("🕒 Temporizador principal")]
    public Timer tiempoEscena;

    [Header("🏁 Condición de victoria")]
    public int recoleccionesNecesarias = 9;

    private int itemsRecolectados = 0;

    private void Start()
    {
        if (textoScore != null)
            textoScore.text = "0";

        if (textoItems != null)
            textoItems.text = $"0 / {recoleccionesNecesarias}";

        if (tiempoEscena != null)
            tiempoEscena.TimerStart();
    }

    private void Update()
    {
        // 🔹 Actualiza el tiempo visual si hay Timer activo
        if (tiempoEscena != null)
        {
            minTMP.text = tiempoEscena.timerMinutes.text;
            segTMP.text = tiempoEscena.timerSeconds.text;
            milSegTMP.text = tiempoEscena.timerSeconds100.text;
        }
    }

    // 🔹 Llamado por cada recolección
    public void RegistrarItemRecolectado(int puntos)
    {
        // Aumentar score e ítems en el GameManager (si existe)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AgregarPuntos(puntos);
            GameManager.Instance.RegistrarColision(); // usamos colisiones como “ítems recolectados”
        }

        // Actualizar el contador local
        itemsRecolectados++;

        // Actualizar textos
        if (textoScore != null)
            textoScore.text = GameManager.Instance.score.ToString();

        if (textoItems != null)
            textoItems.text = $"{itemsRecolectados} / {recoleccionesNecesarias}";

        // Revisar si se cumplió la condición de victoria
        if (itemsRecolectados >= recoleccionesNecesarias)
        {
            FinalizarEscena();
        }
    }

    private void FinalizarEscena()
    {
        Debug.Log("🏁 Todos los ítems recolectados — Escena completada.");

        if (tiempoEscena != null)
            tiempoEscena.TimerStop();

        // Buscar y mostrar el panel final si existe
        PanelFinalController panel = FindFirstObjectByType<PanelFinalController>();
        if (panel != null)
            panel.MostrarPanelFinal();
    }

    // 🔹 Botón de volver al menú
    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        SceneManager.LoadScene("MenuPrincipal"); // Cambia el nombre según tu escena real
    }

    // 🔹 Botón de salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("👋 Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
