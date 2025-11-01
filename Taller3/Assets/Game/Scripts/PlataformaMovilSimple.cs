using UnityEngine;

public class PlataformaMovil_Auto : MonoBehaviour
{
    [Header("Movimiento automático")]
    public Vector3 direccion = Vector3.right; // dirección del movimiento (ej: izquierda/derecha)
    public float distancia = 3f;              // hasta dónde se moverá desde su punto inicial
    public float velocidad = 5f;              // velocidad de movimiento

    private Vector3 puntoInicial;
    private Vector3 puntoDestino;
    private bool yendoADestino = true;

    void Start()
    {
        puntoInicial = transform.position;
        puntoDestino = puntoInicial + direccion.normalized * distancia;
    }

    void Update()
    {
        // Mover hacia el destino actual
        Vector3 objetivo = yendoADestino ? puntoDestino : puntoInicial;
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

        // Si llega, invierte el sentido
        if (Vector3.Distance(transform.position, objetivo) < 0.05f)
        {
            yendoADestino = !yendoADestino;
        }
    }
}
