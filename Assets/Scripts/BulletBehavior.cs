using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 1.0f; // The bullet will destroy itself after this time

    private void Start()
    {
        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject);       // Destroy the bullet
        }
    }
}