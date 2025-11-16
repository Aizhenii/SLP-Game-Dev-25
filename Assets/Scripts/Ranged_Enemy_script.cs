
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
    private Transform baseTarget;

    private Rigidbody2D rb;

    private float attckTimer;


    /// <summary>
    /// initializes a tower object, and  rigidbody
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //GameObject towerObject = GameObject.FindGameObjectWithTag("DefenseTower");
        GameObject baseObject = GameObject.FindGameObjectWithTag("Base");
        // if (towerObject != null)
        // {
        //     towerTarget = towerObject.transform;
        // }
        if(baseObject != null)
        {
            baseTarget = baseObject.transform;
        }

        attckTimer = 0f;
    }

    /// <summary>
    /// updated wether a tower has been located or not, and if it is do a ranged attack
    /// once the enemy is close enough to the tower
    /// </summary>
    private void Update()
    {

        towerTarget = FindTower();


        // if (towerTarget == null && baseTarget == null)
        // {
            // GameObject towerObject = GameObject.FindGameObjectWithTag("DefenseTower");
            // GameObject baseObject = GameObject.FindGameObjectWithTag("Base");

            // if (towerObject != null)
            // {
            //     towerTarget = towerObject.transform;
            // }
            // if (baseObject != null)
            // {
            //     baseTarget = baseObject.transform;
            // }
            if (towerTarget == null && baseTarget == null)
            {
                EnemyPathing();
                return;
            }
                
        // }

        EnemyPathing();

        float distanceToObject = float.MaxValue;

        if (towerTarget != null) {
            float distanceToTower = Vector2.Distance(transform.position, towerTarget.position);
            if(distanceToTower < distanceToObject)
            {
                distanceToObject = distanceToTower;
            }
        }
        if (baseTarget != null) {
            float distanceToBase = Vector2.Distance(transform.position, baseTarget.position);
            if(distanceToBase < distanceToObject)
            {
                distanceToObject = distanceToBase;
            }
        }
        HandleRangedAttack(distanceToObject);
         

    }


    private Transform FindTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("DefenseTower");

        if(towers == null || towers.Length == 0)
        {
            return null;
        }

        Transform closest = null;
        float closestDistance = float.MaxValue;
        Vector2 pos = transform.position;

        foreach(GameObject tower in towers)
        {
            if(tower == null || !tower.activeInHierarchy)
            {
                continue;
            }
            float distance = Vector2.Distance(pos, tower.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closest = tower.transform;
            }
        }
        return closest;


    }


    /// <summary>
    /// it makes sure the enemy is in range of the tower
    /// and if it is to shoot a projectile and begin the cooldown timer
    /// </summary>
    /// <param name="distanceToTower">current distance from tower</param>
    private void HandleRangedAttack(float distanceToTower)
    {
        attckTimer -= Time.deltaTime;

        if (attckTimer > 0f)
        {
            return;
        }
        if (distanceToTower <= attckRange)
        {
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

        Transform target = null;

        float distanceToTower = float.MaxValue;
        float distanceToBase = float.MaxValue;

        if (towerTarget != null)

        {
            distanceToTower = Vector2.Distance(transform.position, towerTarget.position);
        }
        if (baseTarget != null)
        {
            distanceToBase = Vector2.Distance(transform.position, baseTarget.position);
        }

        if(towerTarget != null && distanceToTower <= attckRange)
        {
            target = towerTarget;
        } 
        else if (baseTarget != null && distanceToBase <= attckRange)
        {
            target = baseTarget;
        }
        if(target == null)
        {
            return;
        }


        GameObject projectileInstance = Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D projectileRigidBody = projectileInstance.GetComponent<Rigidbody2D>();

        if (projectileRigidBody == null)
        {
            Debug.LogWarning($"{name}: no rb");
            return;
        }

        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

        projectileRigidBody.velocity = direction * projectileSpd;

        Debug.Log($"{name}: FIREEE");

    }
}

