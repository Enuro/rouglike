using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public float attackCooldown;

    public float lastAttackTime;
    protected AttackBehaviour attackBehaviour;

    public void SetAttackBehaviour(AttackBehaviour newAttackBehaviour)
    {
        attackBehaviour = newAttackBehaviour;
    }

    public void PerformAttack()
    {
        if (attackBehaviour != null)
        {
            attackBehaviour.Attack(this);
        }
    }

    public bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    public void ApplyDamage(IDamageable target)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }
}
