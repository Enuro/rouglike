using UnityEngine;

public class SwordEnemy: Weapon
{
    private void Start()
    {
        SetAttackBehaviour(new OverlapAttack());
    }

    private void Update()
    {
        PerformAttack();
    }
}
