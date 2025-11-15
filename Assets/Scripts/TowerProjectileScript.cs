using UnityEngine;


public class TowerProjectileScript : MonoBehaviour
{
    public float lifeSpan = 3f;

    private Transform target;
    private float damage;

    private float speed;

    public void initialize(Transform targetTransform, float damageAmount, float projectileSpeed)
    {
        target = targetTransform;
        damage = damageAmount;
        speed = projectileSpeed;
    }

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
   }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy")){
            return;
        }
        EnemyScript enemy = other.GetComponent<EnemyScript>();
        if(enemy!= null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}