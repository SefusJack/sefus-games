using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    List<Card> deck;
    public string title = "";
    public int Count = 0;
    public Deck(string t)
    {
        deck = new List<Card>();
        title = t;
    }
    public void Add(Card c)
    {
        deck.Add(c);
        Count++;
    }
    public void Remove(Card c)
    {
        deck.Remove(c);
        Count--;
    }
    public Card getCard(int i)
    {
        return deck[i];
    }
    public void setCard(Card c, int i)
    {
        deck[i] = c;
    }
    public void printCards()
    {
        foreach (Card c in deck)
        {
            c.printInfo();
        }
    }
}
