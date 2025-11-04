using UnityEngine;

public class RotatingSphere : MonoBehaviour
{
    [Header("Rotación")]
    public float velocidadRotacion = 100f;

    [Header("Dirección Aleatoria")]
    public bool direccionAleatoria = true;

    private Vector3 direccion;
    private Rigidbody rb;

    void Start()
    {
        // Configurar Rigidbody automáticamente
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition; // No se mueve, solo gira

        if (direccionAleatoria)
        {
            // Generar dirección aleatoria
            direccion = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;
        }
        else
        {
            // Dirección por defecto
            direccion = Vector3.up;
        }
    }

    void FixedUpdate()
    {
        // Rotar usando Rigidbody para física correcta
        Quaternion deltaRotation = Quaternion.Euler(direccion * velocidadRotacion * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}