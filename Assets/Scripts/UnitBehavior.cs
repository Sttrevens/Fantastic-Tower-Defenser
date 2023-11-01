using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public float shootingInterval = 1.0f;

    private float nextFireTime = 0f;
    private Vector2 shootDirection;
    private Animator animator; // Reference to the Animator

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5.0f, Vector2.zero, detectionRange);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            shootDirection = (hit.collider.transform.position - transform.position).normalized;

            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + shootingInterval;
            }
        }
        else
        {
            // If no enemy is detected, set the attack animation boolean to false
            animator.SetBool("IsAttacking", false);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletSpeed;

        // Trigger the attack animation
        animator.SetBool("IsAttacking", true);
    }
}
