using UnityEngine;

public class Sword : Weapon
{
    private void Start()
    {
        SetAttackBehaviour(new OverlapAttack());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PerformAttack();
        }
    }
}