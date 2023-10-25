using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public int ammo = 1; // Starting ammo amount
    public TMP_Text ammoText; // Reference to the TextMeshProUGUI component to display ammo

    private void Start()
    {
        UpdateAmmoText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ammo > 0) // Check for ammo before shooting
        {
            Shoot();
            ammo--;
            UpdateAmmoText();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player collides with a dice object
        if (other.gameObject.CompareTag("Dice"))
        {
            // Refill ammo by a random amount between 1 and 6
            ammo += Random.Range(0, 7); // Note: Random.Range with integers is inclusive on the min and exclusive on the max
            UpdateAmmoText();

            //Destroy(other.gameObject); // Destroy the dice object after collecting
        }
    }

    void UpdateAmmoText()
    {
        ammoText.text = ammo.ToString();
    }
}