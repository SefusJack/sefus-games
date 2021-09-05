using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Card> Cards;
    public Deck(){}

    public Deck(List<Card> d)
    {
        Cards = d;
    }

    public void addCard(Card c)
    {
        Cards.Add(c);
    }
    public void addCards(List<Card> cs)
    {
        foreach (Card c in cs)
        {
            Cards.Add(c);
        }
    }
}
