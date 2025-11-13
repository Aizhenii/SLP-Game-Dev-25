
using UnityEngine;

/// <summary>
/// Destroys the axe projectile from the
/// construction worker after it hits the tower
/// </summary>
public class ProjectileScript : MonoBehaviour
{
    public float projectilelifeline = 2f;


    private void Start()
    {
        Destroy(gameObject, projectilelifeline);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            Debug.Log($"{name}: poof!!");
            Destroy(gameObject);
        }
    }
}