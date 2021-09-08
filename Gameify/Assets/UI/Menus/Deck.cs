using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> deck;
    public string title = "";
    public int Count = 0;
    public Deck()
    {
        deck = new List<Card>();
    }
    public void Add(Card c)
    {
        deck.Add(c);
        Count++;
    }
    public Card Get(int i)
    {
        return deck[i];
    }
    public void Set(Card c, int i)
    {
        deck[i] = c;
    }
}
