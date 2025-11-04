using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum TipoEnergia { Pequeña, Grande, Fuego }
    [Header("Configuración del ítem")]
    public TipoEnergia tipo = TipoEnergia.Pequeña;

    public int valorPequeña = 2;
    public int valorGrande = 5;
    public int valorFuego = 12;


    public AudioClip sonidoRecolectar;
    public GameObject efectoRecolectar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int valor = tipo == TipoEnergia.Pequeña ? valorPequeña :
             tipo == TipoEnergia.Grande ? valorGrande :
             valorFuego;

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
