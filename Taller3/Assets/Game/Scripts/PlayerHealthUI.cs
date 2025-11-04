using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerRespawn playerHealth; // Referencia al script PlayerHealth
    public GameObject[] hearts;       // Corazones en la UI (GameObjects)

    // Llamar cada vez que cambie la vida
    public void UpdateHearts()
    {
        if (playerHealth == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < playerHealth.CurrentLives);
        }
    }
}