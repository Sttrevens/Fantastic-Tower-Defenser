using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteChanger : MonoBehaviour
{
    public List<Sprite> sprites; // Drag and drop your sprites into this list in the Inspector
    public float changeInterval = 1.0f; // Time in seconds between sprite changes

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Count > 0)
        {
            StartCoroutine(ChangeSpriteRoutine());
        }
        else
        {
            Debug.LogWarning("No sprites provided for RandomSpriteChanger on " + gameObject.name);
        }
    }

    private IEnumerator ChangeSpriteRoutine()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
            yield return new WaitForSeconds(changeInterval);
        }
    }
}