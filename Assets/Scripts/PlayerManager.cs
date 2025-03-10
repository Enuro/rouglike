using UnityEngine;

public class PlayerManager : MonoBehaviour, EDamageable
{
    public healthpointBar healthpointBar;
    public bool gameOver;

    private void Start()
    {
        gameOver = false;
    }
    private void Die()
    {
        Debug.Log("Player died!");
    }

    public void ETakeDamage(float damage)
    {
        healthpointBar.healthpoint -= damage;

        if (healthpointBar.healthpoint <= 0)
            Die();
    }
}
