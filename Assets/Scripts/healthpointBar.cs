using UnityEngine;
using UnityEngine.UI;

public class healthpointBat: MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealthpoint = 1000f;
    public float healthpoint;
    private float lerpSpeed = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthpoint = maxHealthpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != healthpoint)
        {
            healthSlider.value = healthpoint;
        }

        if (healthSlider.value != easeHealthSlider.value) 
        { 
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthpoint, lerpSpeed);
        }
    }

    void takeDamage(float damage)
    {
        healthpoint -= damage;
    }
}
