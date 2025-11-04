using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [Header("Movimiento")]
    public Vector3 puntoA;
    public Vector3 puntoB;
    public float velocidad = 2f;

    private Vector3 objetivoActual;

    private void Start()
    {
        objetivoActual = puntoB;
    }

    private void Update()
    {
        // Mueve el cubo entre A y B constantemente
        transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivoActual) < 0.05f)
        {
            objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;
        }
    }
}