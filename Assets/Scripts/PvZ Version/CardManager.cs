using System.Collections.Generic;
using TMPro;  // Namespace for TextMesh Pro
using UnityEngine;
using UnityEngine.UI;  // Namespace for Unity's UI system

public class CardManager : MonoBehaviour
{
    public Battlefield battlefield;  // Reference to the Battlefield
    public GameObject cardUIPrefab;  // Prefab representing the card in UI
    public Transform cardHandTransform;  // Parent transform for cards in hand
    public List<Card> availableCards = new List<Card>();  // List of all available cards in the game

    private CardDeck cardDeck;  // Assuming you have a CardDeck class to manage the deck
    private List<Card> currentHand = new List<Card>();

    void Start()
    {
        cardDeck = new CardDeck(availableCards);  // Initialize the card deck with the list of available cards
        DrawInitialHand();
    }

    void DrawInitialHand()
    {
        currentHand = cardDeck.DrawHand();

        foreach (Card card in currentHand)
        {
            GameObject cardInstance = Instantiate(cardUIPrefab, cardHandTransform);
            cardInstance.GetComponentInChildren<TextMeshProUGUI>().text = card.cardName;
            cardInstance.GetComponent<Button>().onClick.AddListener(() => PlayCard(card));
        }
    }

    void PlayCard(Card card)
    {
        battlefield.PrepareToPlacePlant(card.unitPrefab);
        currentHand.Remove(card);
        // If you're keeping a reference to the UI game object inside the Card class, you can destroy it here.
        //Destroy(card.gameObject);
    }
}