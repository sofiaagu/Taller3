using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    // Propiedad pública de solo lectura
    public int CurrentLives
    {
        get { return currentLives; }
    }

    public PlayerHealthUI playerHealthUI; // Asignar en el Inspector

    void Start()
    {
        currentLives = maxLives;
        if (playerHealthUI != null)
            playerHealthUI.UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;

        if (currentLives < 0)
            currentLives = 0;

        Debug.Log("Vidas restantes: " + currentLives);

        if (playerHealthUI != null)
            playerHealthUI.UpdateHearts();

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador muerto");
        // Aquí puedes reiniciar la escena o mostrar Game Over
        // Ejemplo:
        // UnityEngine.SceneManagement.SceneManager.LoadScene("NombreEscena");
    }
}
