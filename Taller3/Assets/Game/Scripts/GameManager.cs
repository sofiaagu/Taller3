using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton global

    // === Variables globales ===
    private float globalTime = 0f;  // Tiempo total entre escenas
    private int score = 0;          // Puntaje acumulado
    private int itemsCount = 0;     // Ítems recolectados (energías)

    // === Propiedades públicas ===
    public float GlobalTime => globalTime;
    public int Score => score;
    public int ItemsCount => itemsCount;

    // === Singleton ===
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    // === Métodos de actualización ===

    /// <summary>
    /// Suma tiempo al contador global.
    /// </summary>
    public void AddTime(float timeScene)
    {
        globalTime += timeScene;
    }

    /// <summary>
    /// Suma puntaje al total global.
    /// </summary>
    public void AddScore(int scoreItem)
    {
        score += scoreItem;
    }

    /// <summary>
    /// Incrementa el número de ítems recolectados.
    /// </summary>
    public void AddItem()
    {
        itemsCount++;
    }

    /// <summary>
    /// Reinicia todas las variables (si el jugador vuelve al menú).
    /// </summary>
    public void ResetGame()
    {
        globalTime = 0f;
        score = 0;
        itemsCount = 0;
    }

    /// <summary>
    /// Cierra completamente el juego.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    /// <summary>
    /// Carga una escena por nombre (útil para botones del menú).
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
