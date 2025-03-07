using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider easeStaminaSlider;
    public float maxStamina = 100f;
    public float stamina;
    private float lerpSpeed = 0.1f;
    private float staminaRegenRate = 15f; // Скорость восстановления
    private float staminaDrain = 20f; // Расход при действии

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        if (staminaSlider.value != stamina)
        {
            staminaSlider.value = stamina;
        }

        // Использование выносливости при нажатии Shift (например, для бега)
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= staminaDrain * Time.deltaTime; // Тратим выносливость
        }
        else if (stamina < maxStamina) // Если не жмем Shift, выносливость восстанавливается
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        // Плавное изменение анимации стамины
        if (staminaSlider.value != easeStaminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, stamina, lerpSpeed);
        }

        // Ограничение выносливости
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
