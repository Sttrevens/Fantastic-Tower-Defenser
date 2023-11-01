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
    public float delayBetweenStarts = 0.5f;

    public List<GameObject> enemyPrefabs;
    
    public Transform spawnPoint; // Drag the spawn point here
    public float fadeEffectDuration = 10f; // Duration for which the fade effect runs

    private bool isFading = false;
    private bool continueFading = true;

    public int spawnRangefrom = 2;
    public int spawnRangeto = 10;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRangefrom, spawnRangeto));
            if (enemyPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("No enemy prefabs assigned");
            }
            

            // Start the fade effect when an enemy is spawned
            StartCoroutine(FadeEffect());
        }
    }

    IEnumerator FadeEffect()
    {
        if (!isFading) // Ensure the effect isn't already running
        {
            isFading = true;
            continueFading = true;
            StartCoroutine(FadeLoop());

            // Stop the fade effect after a certain duration
            yield return new WaitForSeconds(fadeEffectDuration);
            continueFading = false;
        }
    }

    IEnumerator FadeLoop()
    {
        while (continueFading)
        {
            foreach (var obj in objectsToFade)
            {
                StartCoroutine(FadeObjectInOut(obj));
                yield return new WaitForSeconds(delayBetweenStarts);
            }

            yield return new WaitForSeconds(fadeInDuration + displayDuration + fadeOutDuration);
        }
        isFading = false;
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