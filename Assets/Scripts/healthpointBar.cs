using UnityEngine;
using UnityEngine.UI;

public class healthpointBar: MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealthpoint = 1000f;
    public float healthpoint;
    private float lerpSpeed = 0.05f;

    void Start()
    {
        healthpoint = maxHealthpoint;
    }

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
}
