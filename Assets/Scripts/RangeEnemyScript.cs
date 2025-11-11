using UnityEngine;

public class RangeEnemyScript : EnemyScript
{
    public float attckRange = 2f;
    public float attckCoolDown = 2f;

    public float projectileSpd = 4f;

    public GameObject projectilePrefab;

    private Transform towerTarget;

    private float attckTimer;



    private void Awake()
    {
        GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");
        if (towerObject != null)
        {
            towerTarget = towerObject.transform;
        }

        attckTimer = 0;
    }


    private void Update()
    {
        EnemyPathing();
        HandleRangedAttack();
    }


    private void HandleRangedAttack()
    {
        if (towerTarget == null)
        {
            GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");
            if (towerObject != null)
            {
                towerTarget = towerObject.transform;
            }
        }

        if (towerTarget == null)
        {
            return;
        }

        attckTimer -= Time.deltaTime;
        if (attckTimer > 0f)
        {
            return;
        }

        float distanceToTower = Vector2.Distance(transform.position, towerTarget.position);
        if (distanceToTower <= attckRange)
        {
            Rigidbody2D rb = getComponent<RigidBody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            RangeAttack();
            attckTimer = attckCoolDown;
        }
    }
    
    public void RangeAttack()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"{name}: No projectile");
            return;
        }

        GameObject projectileInstance = Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidBody = projectileInstance.GetComponent<Rigidbody2D>();
        if (projectileRigidBody == null)
        {
            Debug.LogWarning($"{name}: no rb");
            return;
        }

        Vector2 direction = ((Vector2)towerTarget.position - (Vector2)transform.position).normalized;
        projectileRigidBody.velocity = direction * projectileSpd;

        Debug.Log($"{name}: FIREEE");
        
    }
}