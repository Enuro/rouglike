using UnityEngine;

public class OverlapAttack : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        if (weapon.CanAttack())
        {
            Debug.Log("Overlap attack!");
            weapon.lastAttackTime = Time.time;

            Collider[] hitColliders = Physics.OverlapSphere(weapon.transform.position, weapon.attackRange);

            foreach (var hitCollider in hitColliders)
            {

                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    weapon.ApplyDamage(damageable);
                }
            }

            Debug.DrawLine(weapon.transform.position, weapon.transform.position + weapon.transform.forward * weapon.attackRange, Color.green, 1f);
        }
    }
}