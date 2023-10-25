using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint; // Point from where the bullet will be instantiated
    public float shootingInterval = 1.0f; // Time in seconds between shots

    private float nextFireTime = 0f; // Used for shooting interval
    private Vector2 shootDirection;

    void Update()
    {
        // Detect enemy within a certain range
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5.0f, Vector2.zero, detectionRange);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Determine the direction to the enemy
            shootDirection = (hit.collider.transform.position - transform.position).normalized;

            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + shootingInterval; // Set the next time the unit can fire
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;
    }
}