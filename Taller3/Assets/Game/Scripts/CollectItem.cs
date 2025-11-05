using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum TipoEnergia { Pequeña, Grande, Fuego }

    [Header("Configuración del ítem")]
    public TipoEnergia tipo = TipoEnergia.Pequeña;

    [Header("Valores por tipo")]
    public int valorPequeña = 2;
    public int valorGrande = 5;
    public int valorFuego = 12;

    [Header("Efectos opcionales")]
    public AudioClip sonidoRecolectar;
    public GameObject efectoRecolectar;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int valor = 0;

        // 🔹 Determinar el valor del ítem según su tipo
        switch (tipo)
        {
            case TipoEnergia.Pequeña:
                valor = valorPequeña;
                break;
            case TipoEnergia.Grande:
                valor = valorGrande;
                break;
            case TipoEnergia.Fuego:
                valor = valorFuego;
                break;
        }

        // Sumar al puntaje e incrementar contador global de ítems
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AgregarPuntos(valor);
            GameManager.Instance.RegistrarItem(); // aquí se cuenta la esfera
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el GameManager en la escena.");
        }

        // 🔹 Reproducir sonido
        if (sonidoRecolectar != null)
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

        // 🔹 Crear efecto visual
        if (efectoRecolectar != null)
            Instantiate(efectoRecolectar, transform.position, Quaternion.identity);

        // 🔹 Eliminar el ítem
        Destroy(gameObject);
    }
}
