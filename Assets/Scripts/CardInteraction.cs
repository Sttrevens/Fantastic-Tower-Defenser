using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI

public class CardInteraction : MonoBehaviour
{
    public Image cardImage; // Drag the UI Image component here in the Inspector
    public Sprite[] cardSprites; // List of sprites that the Image can be changed to
    public GameObject unitPrefab; // Drag the Unit prefab here in the Inspector

    void Update()
    {
        if (cardImage.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.J))
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        Instantiate(unitPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player collides with a card object
        if (other.gameObject.CompareTag("Card"))
        {
            ChangeCardImage();
        }
    }

    void ChangeCardImage()
    {
        int randomIndex = Random.Range(0, cardSprites.Length);
        cardImage.sprite = cardSprites[randomIndex]; // Set the image to a random sprite from the list
        cardImage.gameObject.SetActive(true); // Make sure the image is active
    }
}