using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum TipoEnergia { Pequeña, Grande }
    [Header("Configuración del ítem")]
    public TipoEnergia tipo = TipoEnergia.Pequeña;

    public int valorPequeña = 2;
    public int valorGrande = 5;

    public AudioClip sonidoRecolectar;
    public GameObject efectoRecolectar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int valor = tipo == TipoEnergia.Pequeña ? valorPequeña : valorGrande;

            // Sumar puntos al GameManager (si existe)
            //if (GameManager.instance != null)
            //    GameManager.instance.SumarPuntos(valor);

            // Reproducir sonido (si hay)
            if (sonidoRecolectar != null)
                AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

            // Efecto visual (si hay)
            if (efectoRecolectar != null)
                Instantiate(efectoRecolectar, transform.position, Quaternion.identity);

            // Destruir el objeto
            Destroy(gameObject);
        }
    }
}
