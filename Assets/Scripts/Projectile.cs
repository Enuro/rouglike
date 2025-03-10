using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage; 
    private float range; 
    private Vector3 direction; 
    private Vector3 startPosition; 

    public void Initialize(float damage, float range, Vector3 direction)
    {
        this.damage = damage;
        this.range = range;
        this.direction = direction.normalized;
        startPosition = transform.position;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float speed = 10f; 
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}