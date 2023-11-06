using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform healthBarRect;
    private float originalScaleX; // To keep the original X scale

    void Awake()
    {
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }

        healthBarRect = GetComponent<RectTransform>();
        originalScaleX = healthBarRect.localScale.x; // Store the original X scale
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(true);
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health is within bounds

        // Calculate the new scale based on the current health
        float scaleX = (currentHealth / maxHealth) * originalScaleX;
        healthBarRect.localScale = new Vector3(scaleX, healthBarRect.localScale.y, healthBarRect.localScale.z);
    }
}
