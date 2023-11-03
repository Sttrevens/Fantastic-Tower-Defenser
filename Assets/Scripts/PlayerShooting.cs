using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public TMP_Text ammoText; // Reference to display cooldown
    public bool freeAimingEnabled = true; // Set this to true if free aiming is allowed

    private float shootCooldown = 1.0f; // Cooldown time between shots
    private float nextShootTime = 0f; // When the player can shoot next
    private bool isUnlimitedShootingActive = false;

    public GameObject unitPrefab;

    void Update()
    {
        // Allow shooting based on cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Shoot();
            if (!isUnlimitedShootingActive)
            {
                nextShootTime = Time.time + shootCooldown; // Set the next allowed shooting time
            }
        }

        if (freeAimingEnabled)
        {
            HandleAiming();
        }
        else
        {
            firePoint.right = Vector2.right; // Always aim to the right if free aiming is disabled
        }

        // Check for card activation
        if (Input.GetKeyDown(KeyCode.E))
        {
            CardInteraction.instance.UseCard();
        }
    }

    void HandleAiming()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        // Shoot in the direction of the fire point if free aiming, otherwise to the right
        Vector2 shootingDirection = freeAimingEnabled ? firePoint.right : Vector2.right;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, freeAimingEnabled ? firePoint.eulerAngles.z : 0));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * bulletSpeed;
    }

    public void ActivateUnlimitedShooting()
    {
        isUnlimitedShootingActive = true;
        StartCoroutine(DeactivateUnlimitedShooting());
    }

    private IEnumerator DeactivateUnlimitedShooting()
    {
        yield return new WaitForSeconds(5f);
        isUnlimitedShootingActive = false;
    }
}