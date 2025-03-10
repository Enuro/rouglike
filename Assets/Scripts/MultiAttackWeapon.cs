using UnityEngine;

public class MultiAttackWeapon : Weapon
{
    public enum AttackType
    {
        Overlap,
        Raycast,
        Projectile
    }

    public StaminaBar staminaBar;
    public AttackType currentAttackType;

    private void Start()
    {
        SetAttackBehaviour(GetAttackBehaviour(currentAttackType));
    }
    private void SetAttackType(AttackType newAttackType)
    {
        currentAttackType = newAttackType;
        SetAttackBehaviour(GetAttackBehaviour(newAttackType));
        Debug.Log("Switched to " + newAttackType + " attack!");
    }
    private AttackBehaviour GetAttackBehaviour(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Overlap:
                return new OverlapAttack();
            case AttackType.Raycast:
                return new RaycastAttack();
            case AttackType.Projectile:
                return new ProjectileAttack();
            default:
                return new OverlapAttack(); // По умолчанию
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Цифра 1
        {
            SetAttackType(AttackType.Overlap);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Цифра 2
        {
            SetAttackType(AttackType.Raycast);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Цифра 3
        {
            SetAttackType(AttackType.Projectile);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();

            if (currentAttackType == AttackType.Overlap)
                staminaBar.stamina -= 10f;
        }
    }
}
