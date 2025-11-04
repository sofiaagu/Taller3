using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public enum TipoEnergia { Pequeña, Grande }

    [Header("Configuración del ítem")]
    public TipoEnergia tipo = TipoEnergia.Pequeña;
    public int valorPequeña = 2;
    public int valorGrande = 5;

    [Header("Efectos opcionales")]
    public AudioClip sonidoRecolectar;
    public GameObject efectoRecolectar;

    private void OnTriggerEnter(Collider other)
    {
        // Solo el jugador puede recoger
        if (other.CompareTag("Player"))
        {
            int valor = tipo == TipoEnergia.Pequeña ? valorPequeña : valorGrande;

            // 🔹 Notificar al ControllerScene2 que se ha recolectado un ítem
            ControllerScene2 scene2 = FindFirstObjectByType<ControllerScene2>();
            if (scene2 != null)
                scene2.RegistrarItemRecolectado(valor);

            // 🔹 Reproducir sonido
            if (sonidoRecolectar != null)
                AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

            // 🔹 Instanciar efecto visual
            if (efectoRecolectar != null)
                Instantiate(efectoRecolectar, transform.position, Quaternion.identity);

            // 🔹 Eliminar el objeto de la escena
            Destroy(gameObject);
        }
    }
}
