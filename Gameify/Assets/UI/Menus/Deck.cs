using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Deck
{
    List<Card> deck;
    public string title = "";
    public int Count = 0;
    bool newcard = false;
    public Deck(string t)
    {
        deck = new List<Card>();
        title = t;
    }
    public void Add(Card c)
    {
        deck.Add(c);
        Count++;
        newcard = true;
    }
    public void Remove(Card c)
    {
        deck.Remove(c);
        Count--;
        getNewCards();
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
    public void incrementCorrect(int i)
    {
        deck[i].correct++;
    }
    public void incrementWrong(int i)
    {
        deck[i].wrong++;
    }
    public void updateAverageCardTime(int i, float time)
    {
        deck[i].updateAverageTime(time);
    }
    public float getAverageCardTime(int i)
    {
        return deck[i].getAverageTime();
    }
    public int IndexOf(Card c)
    {
        return deck.IndexOf(c);
    }
    public bool hasNewCards()
    {
        getNewCards();
        return newcard;
    }

    public List<Card> getCardsByCorrect(int range)
    {
        List<Card> temp = new List<Card>();
        List<float> ratios = new List<float>();
        temp = deck;
        foreach (Card c in deck)
        {
            ratios.Add(c.getRatio());
        }
        temp = temp.OrderByDescending(c => c.getRatio()).ToList();
        return temp.GetRange(0, range);
    }
    public List<Card> getCardsByWrong(int range)
    {
        List<Card> temp = new List<Card>();
        List<float> ratios = new List<float>();
        temp = deck;
        foreach (Card c in deck)
        {
            ratios.Add(c.getRatio());
        }
        temp = temp.OrderBy(c => c.getRatio()).ToList();
        return temp.GetRange(0, range);
    }
    public List<Card> getMediumCards()
    {
        List<Card> temp = new List<Card>();
        foreach (Card c in deck)
        {
            if (!c.isMature() && !c.isHard() && !c.isNew())
            {
                temp.Add(c);
            }
        }
        return temp;
    }
    public List<Card> getHardCards()
    {
        List<Card> temp = new List<Card>();
        foreach (Card c in deck)
        {
            if (c.isHard())
            {
                temp.Add(c);
            }
        }
        return temp;
    }
    public List<Card> getMatureCards()
    {
        List<Card> temp = new List<Card>();
        foreach (Card c in deck)
        {
            if (c.isMature())
            {
                temp.Add(c);
            }
        }
        return temp;
    }
    public List<Card> getNotMatureCards()
    {
        List<Card> temp = new List<Card>();
        foreach (Card c in deck)
        {
            if (!c.isMature())
            {
                temp.Add(c);
            }
        }
        return temp;
    }
    public List<Card> getNewCards()
    {
        List<Card> temp = new List<Card>();
        foreach (Card c in deck)
        {
            if (c.isNew())
            {
                temp.Add(c);
            }
        }
        if (temp.Count == 0)
            newcard = false;
        else
            newcard = true;
        return temp;
    }
}
