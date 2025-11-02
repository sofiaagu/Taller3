using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float fallLimit = -10f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Si no asignas un punto de respawn, toma la posición inicial
        if (respawnPoint == null)
        {
            GameObject spawn = new GameObject("SpawnPoint");
            spawn.transform.position = transform.position;
            respawnPoint = spawn.transform;
        }
    }

    private void Update()
    {
        // Si el jugador cae por debajo del límite, reinicia
        if (transform.position.y < fallLimit)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;
    }
}
