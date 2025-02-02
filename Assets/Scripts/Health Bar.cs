using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public float maxHealth = 100f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(8);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
