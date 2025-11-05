using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelFinalController : MonoBehaviour
{
    [Header("📋 Referencias UI")]
    public GameObject panelFinal;
    public TextMeshProUGUI textoScoreTotal;
    public TextMeshProUGUI textoItemsTotal;
    public TextMeshProUGUI textoTiempoTotal;
    public TextMeshProUGUI textoColisionesTotal;

    private void Start()
    {
        if (panelFinal != null)
            panelFinal.SetActive(false);
    }

    public void MostrarPanelFinal()
    {
        if (panelFinal == null)
        {
            Debug.LogWarning("⚠️ PanelFinal no asignado.");
            return;
        }

        panelFinal.SetActive(true);

        // 🔹 Actualiza datos del GameManager
        if (GameManager.Instance != null)
        {
            textoScoreTotal.text = GameManager.Instance.score.ToString();
            int totalItems = GameManager.Instance.itemsFuego + GameManager.Instance.itemsHielo;
            textoItemsTotal.text = totalItems.ToString();
            textoTiempoTotal.text = GameManager.Instance.tiempoTotal.ToString("F2") + " s";
            textoColisionesTotal.text = GameManager.Instance.colisionesTotales.ToString();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("✅ Panel Final mostrado correctamente");
    }

    public void BotonVolverAlMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.VolverAlMenuYResetear();
    }

    public void BotonSalir()
    {
        Application.Quit();
        Debug.Log("🚪 Saliendo del juego...");
    }
}
