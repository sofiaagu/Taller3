using UnityEngine;
using UnityEngine.UI;

public class OpcionesManager : MonoBehaviour
{
    [Header("Sliders de Opciones")]
    public Slider sliderVolumenGeneral;
    public Slider sliderBrillo;

    [Header("Paneles")]
    public GameObject panelOpciones;
    public GameObject panelMenuPrincipal;

    private void Start()
    {
        // Cargar configuraciones guardadas
        sliderVolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
        sliderBrillo.value = PlayerPrefs.GetFloat("Brillo", 1f);

        AplicarVolumen(sliderVolumenGeneral.value);
        AplicarBrillo(sliderBrillo.value);
    }

    // 🔹 FUNCIONES DE OPCIONES

    public void AplicarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("VolumenGeneral", valor);
    }

    public void AplicarBrillo(float valor)
    {
        // Ejemplo simple: ajusta el color global de la escena
        RenderSettings.ambientLight = Color.white * valor;
        PlayerPrefs.SetFloat("Brillo", valor);
    }

    public void GuardarYVolver()
    {
        PlayerPrefs.Save();
        panelOpciones.SetActive(false);
        panelMenuPrincipal.SetActive(true);
        Debug.Log("Configuraciones guardadas y retorno al menú");
    }

    public void MostrarOpciones()
    {
        panelOpciones.SetActive(true);
        panelMenuPrincipal.SetActive(false);
    }
}
