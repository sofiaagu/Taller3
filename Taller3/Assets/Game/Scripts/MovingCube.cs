using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class MovingCube : MonoBehaviour
{
    [Header("Movimiento")]
    public Vector3 puntoA;
    public Vector3 puntoB;
    public float velocidad = 2f;

    [Header("UI TMP (contador global)")]
    public TextMeshProUGUI contadorTMP; // Asigna un texto TMP desde el Canvas

    private Vector3 objetivoActual;

    // 🔸 Variable estática: es compartida entre todos los cubos
    private static int contadorGlobal = 0;

    private void Start()
    {
        objetivoActual = puntoB;

        // Mostrar valor inicial en pantalla
        if (contadorTMP != null)
            contadorTMP.text = "Colisiones: " + contadorGlobal;
    }

    private void Update()
    {
        // Movimiento entre los dos puntos
        transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivoActual) < 0.05f)
        {
            objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Empuja al jugador (si tiene Rigidbody)
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDir = (collision.transform.position - transform.position).normalized;
                rb.AddForce(pushDir * 5f, ForceMode.Impulse);
            }

            // 🔸 Incrementar contador global
            contadorGlobal++;

            // Actualizar texto en pantalla (solo si está asignado)
            if (contadorTMP != null)
                contadorTMP.text = "Colisiones: " + contadorGlobal;
        }
    }
}
