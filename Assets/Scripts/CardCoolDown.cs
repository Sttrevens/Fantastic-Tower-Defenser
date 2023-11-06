using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardCoolDown : MonoBehaviour
{
    public Transform cooldownBarRect;
    private float originalScaleY; // To keep the original Y scale
    private float newYScale;

    void Awake()
    {
        originalScaleY = 1f; 
        float startScaleY = 0f; // Start from 0 scale
        cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, startScaleY, cooldownBarRect.localScale.z);
    }

    void Update()
    {

    }

    public void SetCoolDownProgress(float current, float max)
    {
        current = Mathf.Clamp(current, 0, max); // Ensure current is within bounds

        // Calculate the new scale based on the current progress
        float scaleY = (current / max) * originalScaleY;
        cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, scaleY, cooldownBarRect.localScale.z);
    }

    public void Enlarge(float duration)
    {

    }

    public IEnumerator EnlargeAndDestroy(GameObject obj, float duration)
    {
        Debug.Log("Coroutine started for object: " + obj.name);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            newYScale = elapsedTime / duration;
            Debug.Log(cooldownBarRect.localScale);
            cooldownBarRect.localScale = new Vector3(cooldownBarRect.localScale.x, newYScale, cooldownBarRect.localScale.z);
            yield return null;
        }

        Debug.Log("Destroying object: " + obj.name);
        Destroy(obj);
    }

    public IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        Image[] images = obj.GetComponentsInChildren<Image>();
        SpriteRenderer[] sprites = obj.GetComponentsInChildren<SpriteRenderer>();

        float elapsedTime = 0;

        Color[] originalImageColors = new Color[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            originalImageColors[i] = images[i].color;
        }

        Color[] originalSpriteColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            originalSpriteColors[i] = sprites[i].color;
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));

            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(originalImageColors[i].r, originalImageColors[i].g, originalImageColors[i].b, alpha);
            }
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] != null)
                {
                    sprites[i].color = new Color(originalSpriteColors[i].r, originalSpriteColors[i].g, originalSpriteColors[i].b, alpha);
                }
            }

            yield return null;
        }

        Destroy(obj);
    }
}