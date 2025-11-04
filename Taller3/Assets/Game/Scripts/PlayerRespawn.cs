using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private Transform respawnPoint;

    [Header("Vida")]
    public int vidasIniciales = 3;
    private int vidasActuales;

    [Header("UI")]
    public GameObject panelPerdiste;
    public PlayerHealthUI healthUI;

    private CharacterController controller;

    public int CurrentLives => vidasActuales;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        vidasActuales = vidasIniciales;

        // Si no hay punto de respawn, crear uno en la posición inicial
        if (respawnPoint == null)
        {
            GameObject spawn = new GameObject("SpawnPoint");
            spawn.transform.position = transform.position;
            respawnPoint = spawn.transform;
        }
    }

    private void Start()
    {
        if (panelPerdiste != null)
            panelPerdiste.SetActive(false);

        if (healthUI != null)
            healthUI.UpdateHearts();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Si toca una zona de muerte o un obstáculo (cubo)
        if (hit.gameObject.CompareTag("DeathZone"))
        {
            PerderVida();
        }
    }

    private void PerderVida()
    {
        vidasActuales--;

        if (healthUI != null)
            healthUI.UpdateHearts();

        Debug.Log("Vida perdida. Vidas restantes: " + vidasActuales);

        if (vidasActuales <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;

        Debug.Log("Respawn");
    }

    private void GameOver()
    {
        Debug.Log("¡Game Over!");

        if (panelPerdiste != null)
            panelPerdiste.SetActive(true);

        Time.timeScale = 0f;
    }
}