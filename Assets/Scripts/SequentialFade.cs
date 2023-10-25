using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequentialFade : MonoBehaviour
{
    public List<GameObject> objectsToFade;
    public float fadeInDuration = 1.0f;
    public float displayDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float delayBetweenStarts = 0.5f; // Time to wait before starting the fade for the next object

    void Start()
    {
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            foreach (var obj in objectsToFade)
            {
                StartCoroutine(FadeObjectInOut(obj));
                yield return new WaitForSeconds(delayBetweenStarts);
            }
            // After fading all objects, wait for the duration of the last object's fade sequence 
            // before starting the loop again.
            yield return new WaitForSeconds(fadeInDuration + displayDuration + fadeOutDuration);
        }
    }

    IEnumerator FadeObjectInOut(GameObject obj)
    {
        // Fade In
        yield return FadeObject(obj, 0f, 1f, fadeInDuration);
        // Wait for a while when the object is fully visible
        yield return new WaitForSeconds(displayDuration);
        // Fade Out
        yield return FadeObject(obj, 1f, 0f, fadeOutDuration);
    }

    IEnumerator FadeObject(GameObject obj, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Image image = obj.GetComponent<Image>();

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);

            if (spriteRenderer)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }

            if (image)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (spriteRenderer)
        {
            Color finalSpriteColor = spriteRenderer.color;
            finalSpriteColor.a = endAlpha;
            spriteRenderer.color = finalSpriteColor;
        }

        if (image)
        {
            Color finalImageColor = image.color;
            finalImageColor.a = endAlpha;
            image.color = finalImageColor;
        }
    }
}