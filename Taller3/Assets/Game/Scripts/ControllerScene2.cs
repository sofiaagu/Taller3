using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("🏁 Panel final")]
    public PanelFinalController panelFinalController;

    [Header("🏁 Condición de victoria")]
    public int recoleccionesNecesarias = 9;

    private void Start()
    {
        Debug.Log("[ControllerScene2] Iniciado. GameManager.Instance = " + (GameManager.Instance != null));

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

        if (tiempoEscena != null)
            tiempoEscena.TimerStop();

        if (panelFinalController != null)
        {
            panelFinalController.MostrarPanelFinal();
        }
        else
        {
            Debug.LogWarning("⚠️ panelFinalController no asignado en el Inspector.");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
