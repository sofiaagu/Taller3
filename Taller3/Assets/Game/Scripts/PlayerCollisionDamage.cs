using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damageAmount = 1;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
