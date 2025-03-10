using UnityEngine;

public class ProjectileAttack : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        if (weapon.CanAttack())
        {
            Debug.Log("Projectile attack!");
            weapon.lastAttackTime = Time.time;

            GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            projectile.transform.position = weapon.transform.position;
            projectile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            Collider collider = projectile.GetComponent<Collider>();
            collider.isTrigger = true;

            Rigidbody rb = projectile.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            Projectile projectileScript = projectile.AddComponent<Projectile>();
            projectileScript.Initialize(weapon.damage, weapon.attackRange, weapon.transform.forward);
        }
    }
}