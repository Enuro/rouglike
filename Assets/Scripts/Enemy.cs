using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public healthpointBar healthpointBar;

    public void TakeDamage(float damage)
    {
        healthpointBar.healthpoint -= damage;
        if (healthpointBar.healthpoint <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}