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
    public int itemsRecogidos = 0;

    [Header("Nombres de escenas")]
    public string escenaNivel1 = "Fuego";
    public string escenaNivel2 = "Hielo";
    public string escenaMenu = "Menu";

    [Header("Datos del juego")]
    public int score = 0;
    public int colisionesTotales = 0;

    [Header("UI Opcional")]
    public TextMeshProUGUI tValue; // Texto para mostrar el puntaje en pantalla

    [Header("Audio General")]
    public AudioSource musicaSource;
    public AudioClip musicaMenu;
    public AudioClip musicaFuego;
    public AudioClip musicaHielo;

    private string escenaActual = "";

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
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
        // Si hay paneles (significa que estamos en el menú principal)
        if (panelPrincipal != null && panelNiveles != null)
        {
            panelPrincipal.SetActive(true);
            panelNiveles.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Suscribirse al evento de cambio de escena
        SceneManager.activeSceneChanged += CambiarMusicaSegunEscena;

        // Reproducir música del menú al iniciar
        CambiarMusicaSegunEscena(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }

    private void CambiarMusicaSegunEscena(Scene escenaAnterior, Scene nuevaEscena)
    {
        escenaActual = nuevaEscena.name;

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
            Debug.Log("🎵 Reproduciendo música de: " + escenaActual);
        }
    }

    // 🔹 GESTIÓN DE PUNTAJE Y COLISIONES
    public void AgregarPuntos(int cantidad)
    {
        score += cantidad;
        Debug.Log("Puntaje actual: " + score);

        if (tValue != null)
            tValue.text = score.ToString();
    }

    public void RegistrarItem()
    {
        itemsRecogidos++;
        Debug.Log("Items recogidos: " + itemsRecogidos);
    }

    public void RegistrarColision()
    {
        colisionesTotales++;
        Debug.Log("Colisiones totales: " + colisionesTotales);
    }

    // 🔹 MENÚ PRINCIPAL
    public void MostrarPanelNiveles()
    {
        if (panelPrincipal == null || panelNiveles == null) return;

        panelPrincipal.SetActive(false);
        panelNiveles.SetActive(true);
        Debug.Log("Mostrando panel de niveles");
    }

    public void VolverAlMenu()
    {
        if (panelPrincipal == null || panelNiveles == null)
        {
            SceneManager.LoadScene(escenaMenu);
        }
        else
        {
            panelPrincipal.SetActive(true);
            panelNiveles.SetActive(false);
            Debug.Log("Volviendo al menú principal");
        }
    }

    // 🔹 CARGA DE NIVELES
    public void IniciarJuego()
    {
        Debug.Log("Iniciando..");
        SceneManager.LoadScene(escenaNivel1);
    }

    public void CargarNivel1()
    {
        Debug.Log("Cargando Nivel 1...");
        SceneManager.LoadScene(escenaNivel1);
    }

    public void CargarNivel2()
    {
        Debug.Log("Cargando Nivel 2...");
        SceneManager.LoadScene(escenaNivel2);
    }

    // 🔹 CONTROL GENERAL DE ESCENAS
    public void ReiniciarJuego()
    {
        Debug.Log("Reiniciando juego...");
        SceneManager.LoadScene(escenaMenu);
        ResetDatos();
    }

    public void ResetDatos()
    {
        score = 0;
        colisionesTotales = 0;

        if (tValue != null)
            tValue.text = "0";
    }

    // 🔹 SALIR DEL JUEGO
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
