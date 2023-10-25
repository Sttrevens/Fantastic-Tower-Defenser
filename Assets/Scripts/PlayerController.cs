using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public LayerMask[] groundLayers; // Layer for platforms and the ground
    public LayerMask oneWayPlatformLayer;
    public float dropThroughTime = 0.2f;
    private float moveInput;
    private Rigidbody2D rb;
    private bool isDroppingThrough = false;
    private int platformLayer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformLayer = Mathf.RoundToInt(Mathf.Log(oneWayPlatformLayer.value, 2));
    }

    void Update()
    {
        isGrounded = false;
        foreach (LayerMask groundLayer in groundLayers)
        {
            if (Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer))
            {
                isGrounded = true;
                break; // exit the loop as we've found the player is grounded
            }
        }

        //Debug.Log("Is Grounded: " + isGrounded);

        // Movement
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(DropThroughPlatform());
        }

        // Drop through platform
        if (isGrounded && !isDroppingThrough)
        {
            if (Input.GetKey(KeyCode.S))
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, false);
            }
        }
    }

    IEnumerator DropThroughPlatform()
    {
        isDroppingThrough = true;

        // Turn off the collision between Player and OneWayPlatform layers
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, true);

        // Wait for a short duration to let the player pass through the platform
        yield return new WaitForSeconds(dropThroughTime);

        // Turn the collision back on
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, false);

        isDroppingThrough = false;
    }
}