using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum TipoEnergia { Fuego, Hielo }

    [Header("Configuración del ítem")]
    public TipoEnergia tipoEnergia;       // Tipo de esfera (🔥 o ❄️)
    public int valorPuntos = 5;           // Cuántos puntos da al jugador

    [Header("Efectos opcionales")]
    public AudioClip sonidoRecoleccion;
    public GameObject efectoVisual;

    private bool recolectado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recolectado) return;
        if (!other.CompareTag("Player")) return;

        recolectado = true;

        // 🔹 Reproduce sonido
        if (sonidoRecoleccion != null)
            AudioSource.PlayClipAtPoint(sonidoRecoleccion, transform.position);

        // 🔹 Efecto visual
        if (efectoVisual != null)
            Instantiate(efectoVisual, transform.position, Quaternion.identity);

        // 🔹 Registrar en GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AgregarPuntos(valorPuntos);

            if (tipoEnergia == TipoEnergia.Fuego)
                GameManager.Instance.RegistrarItemFuego();
            else if (tipoEnergia == TipoEnergia.Hielo)
                GameManager.Instance.RegistrarItemHielo();
        }

        // 🔹 Actualizar el controlador de la escena (si existe)
        ActualizarUI();

        // 🔹 Destruir el objeto recolectado
        Destroy(gameObject);
    }

    private void ActualizarUI()
    {
        // Busca controladores de escena y actualiza la UI según el tipo
        var controllerFuego = FindFirstObjectByType<SceneController1>();
        var controllerHielo = FindFirstObjectByType<ControllerScene2>();

        if (tipoEnergia == TipoEnergia.Fuego && controllerFuego != null)
        {
            controllerFuego.textoItems.text = $"{GameManager.Instance.itemsFuego}";
            controllerFuego.textoScore.text = GameManager.Instance.score.ToString();
        }
        else if (tipoEnergia == TipoEnergia.Hielo && controllerHielo != null)
        {
            controllerHielo.textoItems.text = $"{GameManager.Instance.itemsHielo} / {controllerHielo.recoleccionesNecesarias}";
            controllerHielo.textoScore.text = GameManager.Instance.score.ToString();
        }
    }
}
