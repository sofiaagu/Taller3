using UnityEngine;

public class BotonVolverMenu : MonoBehaviour
{
    // Este método lo llamarás manualmente desde el Inspector
    public void VolverAlMenuClick()
    {
        Debug.Log("🔄 Botón presionado");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.VolverAlMenuYResetear();
        }
        else
        {
            Debug.LogError("❌ GameManager no encontrado");
        }
    }
}