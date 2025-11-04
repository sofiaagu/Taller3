using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal a activar")]
    public GameObject portal; // arrástralo en el inspector

    [Header("Puntos necesarios para activarlo")]
    public int puntosNecesarios = 80;

    private bool portalActivo = false;

    void Update()
    {
        //// Asegúrate de que existe una instancia del GameManager
        //if (GameManager.instance == null || portal == null)
        //    return;

        //// Verifica si ya se cumplió la condición
        //if (!portalActivo && GameManager.instance.score >= puntosNecesarios)
        //{
        //    portal.SetActive(true);
        //    portalActivo = true;
        //}
    }
}
