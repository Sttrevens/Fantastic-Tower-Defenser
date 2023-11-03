using FSM;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public float bulletSpeed = 10f; // This can be removed if you no longer use bullet speed for raycasting
    public Transform firePoint;
    public float shootingInterval = 1.0f;
    public LayerMask enemyLayer; // Define a layer for the enemies to optimize the raycast

    private float nextFireTime = 0f;
    private Animator animator; // Reference to the Animator

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRange, Vector2.zero, detectionRange, enemyLayer);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Vector2 targetDirection = (hit.transform.position - transform.position).normalized;

            if (Time.time > nextFireTime)
            {
                ShootRaycast(targetDirection, hit.transform.gameObject);
                nextFireTime = Time.time + shootingInterval;
            }
        }
        else
        {
            // If no enemy is detected, set the attack animation boolean to false
            animator.SetBool("IsAttacking", false);
        }
    }

    void ShootRaycast(Vector2 direction, GameObject enemyObject)
    {
        // Perform the raycast towards the enemy
        RaycastHit2D shootHit = Physics2D.Raycast(firePoint.position, direction, detectionRange, enemyLayer);
        if (shootHit.collider != null && shootHit.collider.gameObject == enemyObject)
        {
            // Enemy hit, reduce its health
            BaseStateMachine stateMachine = shootHit.collider.GetComponent<BaseStateMachine>();
            if (stateMachine != null)
            {
                stateMachine.BloodMinus();
            }

            // Trigger the attack animation
            animator.SetBool("IsAttacking", true);
        }
    }
}
