using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Paneles del Menú (solo si estás en el menú)")]
    public GameObject panelPrincipal;
    public GameObject panelNiveles;

    [Header("Datos de juego extendidos")]
    public int itemsFuego = 0;
    public int itemsHielo = 0;

    [Header("Nombres de escenas")]
    public string escenaNivel1 = "Fuego";
    public string escenaNivel2 = "Hielo";
    public string escenaMenu = "Menu";

    [Header("Datos del juego")]
    public int score = 0;
    public int colisionesTotales = 0;

    [Header("UI Opcional")]
    public TextMeshProUGUI tValue;

    [Header("Audio General")]
    public AudioSource musicaSource;
    public AudioClip musicaMenu;
    public AudioClip musicaFuego;
    public AudioClip musicaHielo;

    private string escenaActual = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicaSource.volume = 1f;
        AudioListener.volume = 1f;

        if (panelPrincipal != null && panelNiveles != null)
        {
            panelPrincipal.SetActive(true);
            panelNiveles.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.activeSceneChanged += CambiarMusicaSegunEscena;
        CambiarMusicaSegunEscena(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }

    private void CambiarMusicaSegunEscena(Scene anterior, Scene nueva)
    {
        escenaActual = nueva.name;

        if (musicaSource == null) return;

        AudioClip clipSeleccionado = null;

        if (escenaActual == escenaMenu)
            clipSeleccionado = musicaMenu;
        else if (escenaActual == escenaNivel1)
            clipSeleccionado = musicaFuego;
        else if (escenaActual == escenaNivel2)
            clipSeleccionado = musicaHielo;

        if (clipSeleccionado != null && musicaSource.clip != clipSeleccionado)
        {
            musicaSource.Stop();
            musicaSource.clip = clipSeleccionado;
            musicaSource.loop = true;
            musicaSource.Play();
            Debug.Log("🎵 Música: " + escenaActual);
        }
    }

    // 🔹 PUNTOS E ÍTEMS
    public void AgregarPuntos(int cantidad)
    {
        score += cantidad;
        if (tValue != null)
            tValue.text = score.ToString();
    }

    public void RegistrarItemFuego()
    {
        itemsFuego++;
        Debug.Log($"🔥 Ítems de fuego: {itemsFuego}");
    }

    public void RegistrarItemHielo()
    {
        itemsHielo++;
        Debug.Log($"❄️ Ítems de hielo: {itemsHielo}");
    }

    public void RegistrarColision()
    {
        colisionesTotales++;
    }

    public void ResetDatos()
    {
        score = 0;
        colisionesTotales = 0;
        itemsFuego = 0;
        itemsHielo = 0;
        if (tValue != null)
            tValue.text = "0";
    }
}
