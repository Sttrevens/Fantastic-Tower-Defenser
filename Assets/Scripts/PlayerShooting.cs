using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject unitPrefab; // For spawning the unit
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public int ammo = 1; // Starting ammo amount
    public TMP_Text ammoText; // Reference to display ammo

    private bool hasUnlimitedAmmo = false;

    private void Start()
    {
        UpdateAmmoText();
    }

    void Update()
    {
        HandleAiming();

        if (Input.GetMouseButtonDown(0) && (hasUnlimitedAmmo || ammo > 0)) // Also allow shooting if unlimited ammo
        {
            Shoot();
            if (!hasUnlimitedAmmo)
            {
                ammo--;
                UpdateAmmoText();
            }
        }

        // Check for card activation
        if (Input.GetKeyDown(KeyCode.J))
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // since we're using the firePoint's direction
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player collides with a dice object
        if (other.gameObject.CompareTag("Dice"))
        {
            // Refill ammo by a random amount between 1 and 6
            ammo += Random.Range(1, 7); // Note: Random.Range with integers is inclusive on the min and exclusive on the max
            UpdateAmmoText();

            // Destroy the dice object after collecting
            //Destroy(other.gameObject);
        }
    }

    void UpdateAmmoText()
    {
        ammoText.text = ammo.ToString();
    }

    public void ActivateUnlimitedAmmo()
    {
        hasUnlimitedAmmo = true;
        StartCoroutine(DeactivateUnlimitedAmmo());
    }

    private IEnumerator DeactivateUnlimitedAmmo()
    {
        yield return new WaitForSeconds(5f);
        hasUnlimitedAmmo = false;
    }
}