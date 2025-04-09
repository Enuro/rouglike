using UnityEngine;

public class Sword : Weapon
{
    private void Start()
    {
        SetAttackBehaviour(new ProjectileAttack());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
    }
}
