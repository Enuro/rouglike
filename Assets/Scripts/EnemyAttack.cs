using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 100f; 
    public float attackRange = 3f;
    public float attackCooldown = 2f; 
    public LayerMask playerLayer; 

    private float lastAttackTime; 
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (IsPlayerInRange())
            {
                AttackPlayer();
            }
        }
    }

    private bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= attackRange;
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy attacks the player!");
        lastAttackTime = Time.time;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            EDamageable damageable = hitCollider.GetComponent<EDamageable>();
            if (damageable != null)
            {
                damageable.ETakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
