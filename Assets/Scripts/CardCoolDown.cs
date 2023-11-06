using UnityEngine;
using System.Collections;

public class CardCoolDown : MonoBehaviour
{
    public RectTransform cooldownBarRect;
    private float originalScaleY; // To keep the original Y scale

    void Awake()
    {
        originalScaleY = 1f;
    }

    public void SetCoolDownProgress(float current, float max)
    {
        current = Mathf.Clamp(current, 0, max); // Ensure current is within bounds

        // Calculate the new scale based on the current progress
        float scaleY = (current / max) * originalScaleY;
        cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, scaleY, cooldownBarRect.localScale.z);
    }

    public IEnumerator EnlargeAndDestroy(GameObject obj, float duration)
    {
        float elapsedTime = 0;

        // If the current scale is 0, we will animate from 0 to the original Y scale
        float startScaleY = 0f;

        // Ensure the current scale is set to 0 to start with (as it should be, based on your description)
        cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, startScaleY, cooldownBarRect.localScale.z);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newYScale = Mathf.Lerp(startScaleY, originalScaleY, elapsedTime / duration);
            cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, newYScale, cooldownBarRect.localScale.z);
            yield return null;
        }

        Destroy(obj);
    }
}