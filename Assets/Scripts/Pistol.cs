using UnityEngine;

public class Pistol : Weapon
{
    private void Start()
    {
        SetAttackBehaviour(new RaycastAttack());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
    }
}
