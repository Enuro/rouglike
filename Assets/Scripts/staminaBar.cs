using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider easeStaminaSlider;
    public float maxStamina = 100f;
    public float stamina;
    private float lerpSpeed = 0.1f;
    private float staminaRegenRate = 15f; // �������� ��������������
    private float staminaDrain = 20f; // ������ ��� ��������

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

        // ������������� ������������ ��� ������� Shift (��������, ��� ����)
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= staminaDrain * Time.deltaTime; // ������ ������������
        }
        else if (stamina < maxStamina) // ���� �� ���� Shift, ������������ �����������������
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        // ������� ��������� �������� �������
        if (staminaSlider.value != easeStaminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, stamina, lerpSpeed);
        }

        // ����������� ������������
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
