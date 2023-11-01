using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class BaseBehavior : MonoBehaviour
{
    private int enemyCollisions = 0; // Count of enemy collisions
    private SpriteRenderer spriteRenderer; // Sprite renderer for fading effect

    public GameObject failurePanel;

    private void Start()
    {
        Time.timeScale = 1f;
        // Get the sprite renderer attached to the base
        spriteRenderer = GetComponent<SpriteRenderer>();

        failurePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the Enemy tag
        if (collision.CompareTag("Enemy"))
        {
            enemyCollisions++;
            Destroy(collision.gameObject);

            if (enemyCollisions == 1)
            {
                // Fade the base by 50%
                FadeBase(0.5f);
            }
            else if (enemyCollisions == 2)
            {
                // End the game
                EndGame();
            }
        }
    }

    private void FadeBase(float alphaValue)
    {
        // Get the current color of the base and change its alpha
        Color baseColor = spriteRenderer.color;
        baseColor.a = alphaValue;
        spriteRenderer.color = baseColor;
    }

    private void EndGame()
    {
        failurePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}