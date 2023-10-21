using System.Collections.Generic;
using UnityEngine;

public class CardDeck
{
    private List<Card> cards = new List<Card>();
    private List<Card> discardPile = new List<Card>(); // Cards that are used and can be reshuffled into the main deck
    public int handSize = 5;

    // Initialize the card deck with a list of available cards
    public CardDeck(List<Card> availableCards)
    {
        cards.AddRange(availableCards);
        Shuffle();
    }

    // Shuffle the card deck
    public void Shuffle()
    {
        cards.AddRange(discardPile); // Add cards from the discard pile back to the main deck
        discardPile.Clear();

        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    // Draw a hand of cards
    public List<Card> DrawHand()
    {
        List<Card> hand = new List<Card>();
        for (int i = 0; i < Mathf.Min(handSize, cards.Count); i++)
        {
            hand.Add(cards[i]);
        }
        foreach (Card card in hand)
        {
            RemoveCard(card);
            discardPile.Add(card); // Add drawn cards to the discard pile
        }
        return hand;
    }

    // Remove a card from the deck
    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }
}