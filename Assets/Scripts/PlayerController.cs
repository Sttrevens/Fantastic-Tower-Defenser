using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f; // Time interval between shots
    private float nextFire = 0.0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Transform selectedCard; // The current card under the mouse
    private Vector3 originalSize; // To keep the original scale
    private Vector3 enlargedSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        //HandleAiming();
        //HandleShooting();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        CardHover();
        CardSelection();
    }

    void HandleMovement()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void HandleAiming()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.right * bulletSpeed; // since in top down right direction of player will be the forward direction
    }

    public void ReloadScene()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void CardHover()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
        {
            // Check if the object hit is a card
            if (hit.collider.CompareTag("SkeletonCard") || hit.collider.CompareTag("InfiniteAmmoCard") || hit.collider.CompareTag("BulletTimeCard"))
            {
                if (selectedCard != hit.transform)
                {
                    // Reset the previous selected card
                    if (selectedCard != null)
                    {
                        selectedCard.localScale = originalSize;
                    }

                    // Set the new selected card
                    selectedCard = hit.transform;
                    originalSize = selectedCard.localScale;
                    enlargedSize = originalSize * 1.1f; // 10% larger, adjust as needed
                    selectedCard.localScale = enlargedSize;
                }
            }
        }
        else
        {
            // If we're no longer hovering over a card, reset it
            if (selectedCard != null)
            {
                selectedCard.localScale = originalSize;
                selectedCard = null;
            }
        }
    }

    void CardSelection()
    {
        if (Input.GetMouseButtonDown(0) && selectedCard != null)
        {
            // Inform the CardInteraction script
            CardInteraction.instance.BulletHitCard(selectedCard.tag);

            // Optionally destroy the card object after clicking
            Destroy(selectedCard.gameObject);
        }
    }
}