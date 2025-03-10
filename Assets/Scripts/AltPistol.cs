using UnityEngine;

public class AltPistol : Weapon
{
    private void Start()
    {
        SetAttackBehaviour(new ProjectileAttack());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            PerformAttack();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackRange > 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
        }
    }
}
