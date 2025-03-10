using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider easeStaminaSlider;
    public float maxStamina = 100f;
    public float stamina;
    private float lerpSpeed = 0.01f;
    private float staminaRegenRate = 10f; // �������� ��������������
    private float staminaDrain = 20f; // ������ ��� ��������

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        stamina += staminaRegenRate * Time.deltaTime;

        if (staminaSlider.value != stamina)
        {
            staminaSlider.value = stamina;
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= staminaDrain * Time.deltaTime; 
        }

        if (staminaSlider.value != easeStaminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, stamina, lerpSpeed);
        }

        //stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
