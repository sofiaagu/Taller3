using UnityEngine;
using TMPro;

public class ControllerScene2 : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoScore;   // Puntaje total
    public TextMeshProUGUI textoItems;   // Energías recolectadas

    [Header("Referencias de tiempo (desde PanelTime)")]
    public TextMeshProUGUI minTMP;
    public TextMeshProUGUI segTMP;
    public TextMeshProUGUI milSegTMP;

    [Header("Temporizador principal")]
    public Timer tiempoEscena;  // Script Timer que ya controla el tiempo

    private int totalItemsEscena;    // Cantidad total de ítems en la escena
    private int itemsRecolectados;   // Contador interno

    void Start()
    {
        // Contar cuántos ítems hay al inicio (con tag "Energia")
        totalItemsEscena = GameObject.FindGameObjectsWithTag("Energia").Length;

        if (textoItems != null)
            textoItems.text = $"0 / {totalItemsEscena}";

        // Iniciar el temporizador
        if (tiempoEscena != null)
            tiempoEscena.TimerStart();
    }

    void Update()
    {
        // Actualiza el puntaje global
        if (textoScore != null)
            textoScore.text = GameManager.instance.Score.ToString();

        // Sincroniza los valores de tiempo del Timer con el panel
        if (tiempoEscena != null)
            ActualizarTiempoUI();
    }

    private void ActualizarTiempoUI()
    {
        // Toma los textos del Timer y los refleja en tu panel
        if (minTMP != null && tiempoEscena.timerMinutes != null)
            minTMP.text = tiempoEscena.timerMinutes.text;

        if (segTMP != null && tiempoEscena.timerSeconds != null)
            segTMP.text = tiempoEscena.timerSeconds.text;

        if (milSegTMP != null && tiempoEscena.timerSeconds100 != null)
            milSegTMP.text = tiempoEscena.timerSeconds100.text;
    }

    // 🔹 Llamar cuando se recolecte un ítem azul
    public void RegistrarItemRecolectado(int puntos)
    {
        itemsRecolectados++;
        GameManager.instance.AddScore(puntos);
        GameManager.instance.AddItem();

        if (textoItems != null)
            textoItems.text = $"{itemsRecolectados} / {totalItemsEscena}";

        // Si ya recogió todos, se termina la escena
        if (itemsRecolectados >= totalItemsEscena)
        {
            FinalizarEscena();
        }
    }

    // 🔹 Detiene el tiempo y lo guarda en el GameManager
    public void TimeScene()
    {
        if (tiempoEscena != null)
        {
            tiempoEscena.TimerStop();
            GameManager.instance.AddTime(tiempoEscena.StopTime);
        }
    }

    // 🔹 Lógica final de escena
    private void FinalizarEscena()
    {
        TimeScene();
        Debug.Log("Escena completada. Puntaje total: " + GameManager.instance.Score);
        // Aquí puedes cargar una escena final o mostrar el panel de resultados
    }
}
