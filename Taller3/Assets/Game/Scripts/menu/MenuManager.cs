using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelPrincipal;
    public GameObject panelNiveles;

    [Header("Nombres de escenas")]
    public string escenaSiguiente = "Nivel1_Fuego"; // escena principal
    public string escenaNivel1 = "Nivel1_Fuego";
    public string escenaNivel2 = "Nivel2_Hielo";

    private void Start()
    {
        // Mostrar solo el menú principal al iniciar
       

        // Desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 🔹 BOTONES PRINCIPALES

    public void IniciarJuego()
    {
        Debug.Log("Iniciando juego...");
        SceneManager.LoadScene(escenaSiguiente);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MostrarPanelNiveles()
    {
        panelPrincipal.SetActive(false);
        panelNiveles.SetActive(true);
        Debug.Log("Mostrando panel de niveles");
    }

    public void VolverAlMenu()
    {
        panelPrincipal.SetActive(true);
        panelNiveles.SetActive(false);
        Debug.Log("Volviendo al menú principal");
    }

    // 🔹 SELECCIÓN DE NIVELES

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
}