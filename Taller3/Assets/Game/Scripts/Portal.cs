using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject portalVisual; // el modelo o efecto del portal
    public GameManager gameManager; // referencia al GameManager

    private void Start()
    {
        if (portalVisual == null)
            portalVisual = gameObject; // usa el propio objeto si no se asigna
    }

    private void Update()
    {
        if (gameManager == null)
            return;

        // Condición: se muestra si tiene menos de 3 colisiones o más de 80 puntos
        bool mostrar = gameManager.colisionesTotales < 3 || gameManager.score > 80;

        portalVisual.SetActive(mostrar);
    }
}