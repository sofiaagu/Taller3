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
            if (other.CompareTag("Player"))
            {
                int valor = 0;

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


                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AgregarPuntos(valor);
                }
                else
                {
                    Debug.LogWarning(" No se encontró el GameManager en la escena.");
                }
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
}
