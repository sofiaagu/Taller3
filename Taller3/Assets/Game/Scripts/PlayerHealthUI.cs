using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // Referencia al script PlayerHealth
    public GameObject[] hearts;       // Corazones en la UI (GameObjects)

    // Llamar cada vez que cambie la vida
    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Si el índice es menor que las vidas actuales, se muestra
            // Si es igual o mayor, se desactiva
            hearts[i].SetActive(i < playerHealth.CurrentLives);
        }
    }
}
