using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerScene2 : MonoBehaviour
{
    [Header("📊 Referencias UI - HUD del juego")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoItems;

    [Header("⏱️ Referencias de tiempo (desde PanelTime)")]
    public TextMeshProUGUI minTMP;
    public TextMeshProUGUI segTMP;
    public TextMeshProUGUI milSegTMP;

    [Header("🕒 Temporizador principal")]
    public Timer tiempoEscena;

    [Header("🏁 Panel Final - GameObject y sus textos")]
    public GameObject panelFinal;
    public TextMeshProUGUI textoPuntajeFinal;
    public TextMeshProUGUI textoItemsFuegoFinal;
    public TextMeshProUGUI textoItemsHieloFinal;
    public TextMeshProUGUI textoColisionesFinal;
    public TextMeshProUGUI textoTiempoTotalFinal;
    public TextMeshProUGUI textoTiempoEscenaFinal;

    [Header("🏁 Condición de victoria")]
    public int recoleccionesNecesarias = 9;

    private void Start()
    {
        Debug.Log("[ControllerScene2] Iniciado. GameManager.Instance = " + (GameManager.Instance != null));

        // Ocultar panel final al inicio
        if (panelFinal != null)
            panelFinal.SetActive(false);

        if (GameManager.Instance != null)
        {
            if (textoScore != null)
                textoScore.text = GameManager.Instance.score.ToString();
            if (textoItems != null)
                textoItems.text = $"{GameManager.Instance.itemsHielo} / {recoleccionesNecesarias}";
        }

        if (tiempoEscena != null)
            tiempoEscena.TimerStart();
    }

    private void Update()
    {
        if (tiempoEscena != null)
        {
            minTMP.text = tiempoEscena.timerMinutes.text;
            segTMP.text = tiempoEscena.timerSeconds.text;
            milSegTMP.text = tiempoEscena.timerSeconds100.text;
        }
    }

    public void RegistrarItemRecolectado(int puntos)
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.AgregarPuntos(puntos);
        GameManager.Instance.RegistrarItemHielo();

        if (textoScore != null)
            textoScore.text = GameManager.Instance.score.ToString();
        if (textoItems != null)
            textoItems.text = $"{GameManager.Instance.itemsHielo} / {recoleccionesNecesarias}";

        if (GameManager.Instance.itemsHielo >= recoleccionesNecesarias)
        {
            FinalizarEscena();
        }
    }

    private void FinalizarEscena()
    {
        Debug.Log("🏁 FinalizarEscena() ejecutado - Mostrando panel final.");

        // Obtener el tiempo transcurrido ANTES de detener el timer
        float tiempoTranscurrido = 0f;
        if (tiempoEscena != null)
        {
            tiempoTranscurrido = tiempoEscena.timerTime; // ✅ Acceso directo
            tiempoEscena.TimerStop();
        }

        // Llamar al GameManager para mostrar el panel final
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MostrarPanelFinal(
                panelFinal,
                textoPuntajeFinal,
                textoItemsFuegoFinal,
                textoItemsHieloFinal,
                textoColisionesFinal,
                textoTiempoTotalFinal,
                textoTiempoEscenaFinal,
                tiempoTranscurrido
            );
        }
        else
        {
            Debug.LogWarning("⚠️ GameManager.Instance no está disponible.");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FinalizarEscenaDesdeItem()
    {
        FinalizarEscena();
    }

    // 🔹 Botones del Panel Final (se llaman desde los botones en el Inspector)
   
  

    public void BotonVolverAlMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.VolverAlMenuYResetear();
    }
}