using UnityEngine;

public class RaycastAttack : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        if (weapon.CanAttack())
        {
            Debug.Log("Raycast attack!");
            weapon.lastAttackTime = Time.time;

            Ray ray = new Ray(weapon.transform.position, weapon.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, weapon.attackRange))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    weapon.ApplyDamage(damageable);
                }

                Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
            }
        }
    }
}
