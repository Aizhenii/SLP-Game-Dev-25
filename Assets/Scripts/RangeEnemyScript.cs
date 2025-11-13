

using UnityEngine;

/// <summary>
/// handles specialized enemies with projectiles
/// </summary>
public class RangeEnemyScript : EnemyScript
{
    public float attckRange = 2f;
    public float attckCoolDown = 2f;

    public float projectileSpd = 4f;

    public GameObject projectilePrefab;

    private Transform towerTarget;

    private Rigidbody2D rb;

    private float attckTimer;


    /// <summary>
    /// initializes a tower object, and  rigidbody
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");
        if (towerObject != null)
        {
            towerTarget = towerObject.transform;
        }

        attckTimer = 0f;
    }

    /// <summary>
    /// updated wether a tower has been located or not, and if it is do a ranged attack
    /// once the enemy is close enough to the tower
    /// </summary>
    private void Update()
    {
        if (towerTarget == null)
        {
            GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");
            if (towerObject != null)
            {
                towerTarget = towerObject.transform;
            }
            else
            {
                EnemyPathing();
                return;
            }
        }

        float distanceToTower = Vector2.Distance(transform.position, towerTarget.position);

        if (distanceToTower > attckRange)
        {
            EnemyPathing();
        }
        else
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            HandleRangedAttack(distanceToTower);
        }

    }

    /// <summary>
    /// it makes sure the enemy is in range of the tower
    /// and if it is to shoot a projectile and begin the cooldown timer
    /// </summary>
    /// <param name="distanceToTower"></param>
    private void HandleRangedAttack(float distanceToTower)
    {
        attckTimer -= Time.deltaTime;

        if (attckTimer > 0f)
        {
            return;
        }
        if (distanceToTower <= attckRange)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            RangeAttack();
            attckTimer = attckCoolDown;
        }
    }

/// <summary>
/// Actually creates the projectile and shoots it
/// at the tower
/// </summary>
    public void RangeAttack()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"{name}: No projectile");
            return;
        }

        if (towerTarget == null)
        {
            Debug.LogWarning($"{name}: No tower target found");
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