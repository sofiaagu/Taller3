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

    [Header("Tiempo total del juego")]
    public float tiempoTotal = 0f;

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

    public void RegistrarTiempo(float tiempoEscena)
    {
        tiempoTotal += tiempoEscena;
        Debug.Log($"⏱️ Tiempo total acumulado: {tiempoTotal:F2} segundos");
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

    // 🔹 Volver al menú principal
    public void VolverAlMenuYResetear()
    {
        ResetDatos();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(escenaMenu);
        Debug.Log("🏠 Volviendo al menú principal...");
    }

    // 🔹 NUEVO: Abrir panel de niveles
    public void AbrirPanelNiveles()
    {
        if (panelPrincipal != null && panelNiveles != null)
        {
            panelPrincipal.SetActive(false);
            panelNiveles.SetActive(true);
            Debug.Log("📜 Panel de niveles abierto.");
        }
    }

    // 🔹 Volver al panel principal desde el panel de niveles
    public void VolverAlMenuDesdeNiveles()
    {
        if (panelPrincipal != null && panelNiveles != null)
        {
            panelPrincipal.SetActive(true);
            panelNiveles.SetActive(false);
            Debug.Log("⬅️ Volviendo al panel principal del menú.");
        }
    }

    // 🔹 NUEVO: Cargar Nivel 1 (Fuego)
    public void CargarNivel1()
    {
        ResetDatos();
        SceneManager.LoadScene(escenaNivel1);
        Debug.Log("🔥 Cargando Nivel 1...");
    }

    // 🔹 NUEVO: Cargar Nivel 2 (Hielo)
    public void CargarNivel2()
    {
        ResetDatos();
        SceneManager.LoadScene(escenaNivel2);
        Debug.Log("❄️ Cargando Nivel 2...");
    }
    // 🔹 NUEVO: Salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("🚪 Saliendo del juego...");
        Application.Quit();
    }
    // 🔹 NUEVO: Mostrar panel final con estadísticas
    public void MostrarPanelFinal(GameObject panelFinal,
                                   TextMeshProUGUI textoPuntaje,
                                   TextMeshProUGUI textoItemsFuego,
                                   TextMeshProUGUI textoItemsHielo,
                                   TextMeshProUGUI textoColisiones,
                                   TextMeshProUGUI textoTiempoTotal,
                                   TextMeshProUGUI textoTiempoEscena,
                                   float tiempoEscenaActual)
    {
        if (panelFinal == null)
        {
            Debug.LogError("⚠️ Panel Final no está asignado.");
            return;
        }

        // Registrar el tiempo de esta escena
        RegistrarTiempo(tiempoEscenaActual);

        // Actualizar textos con las estadísticas
        if (textoPuntaje != null)
            textoPuntaje.text = $"Puntaje Total: {score}";

        if (textoItemsFuego != null)
            textoItemsFuego.text = $"Items de Fuego: {itemsFuego}";

        if (textoItemsHielo != null)
            textoItemsHielo.text = $"Items de Hielo: {itemsHielo}";

        if (textoColisiones != null)
            textoColisiones.text = $"Colisiones Totales: {colisionesTotales}";

        if (textoTiempoTotal != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTotal / 60f);
            int segundos = Mathf.FloorToInt(tiempoTotal % 60f);
            textoTiempoTotal.text = $"Tiempo Total: {minutos:00}:{segundos:00}";
        }

        if (textoTiempoEscena != null)
        {
            int minutos = Mathf.FloorToInt(tiempoEscenaActual / 60f);
            int segundos = Mathf.FloorToInt(tiempoEscenaActual % 60f);
            textoTiempoEscena.text = $"Tiempo Escena: {minutos:00}:{segundos:00}";
        }

        // Mostrar el panel
        panelFinal.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;

        Debug.Log("🏁 Panel Final mostrado con todas las estadísticas.");
    }


    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= CambiarMusicaSegunEscena;
    }
}

