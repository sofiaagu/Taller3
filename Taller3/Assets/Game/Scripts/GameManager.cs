using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public TextMeshProUGUI tValue;
    public int score = 0;
    public int colisionesTotales = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AgregarPuntos(int cantidad)
    {
        score += cantidad;
        Debug.Log("Puntaje actual: " + score);

        if (tValue != null)
        {
            tValue.text = score.ToString();
        }
    }

    public void RegistrarColision()
    {
        colisionesTotales++;
        Debug.Log("Colisiones totales: " + colisionesTotales);
    }
}
