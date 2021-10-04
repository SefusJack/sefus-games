using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultipleChoice : MonoBehaviour
{
    public GameController gc;
    public GameDeck gd;
    public Deck deck;
    public List<Card> choices;
    public List<int> posindeck;
    public int answerIndex = 0;
    public GameObject cfab;
    public GameObject afab;
    public Transform qspace;
    public Transform cspace;
    public Transform aspace;
    public float hardcardchance = 25f;

    public float rightanswertime = 0;
    public float lastrightanswertime = 0;
    
    public void Start()
    {
        deck = gd.GetComponent<GameDeck>().deck;
        generateRound();
        List<Card> cards = deck.getMatureCards();
    }
    public void generateRound()
    {
        lastrightanswertime = rightanswertime;

        choices = new List<Card>();
        posindeck = new List<int>();
        foreach (Transform child in cspace)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in aspace)
        {
            Destroy(child.gameObject);
        }

        answerIndex = Random.Range(0, 4);
        bool hardcard = Random.Range(0, 100) <= hardcardchance;
        for (int i = 0; i < 4; i++)
        {
            Card randomcard;
            int randomindex;
            if (deck.hasNewCards())
            {
                randomcard = generateNewCard();
                while (choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
                {
                    randomcard = generateNewCard();
                }
            }
            else
            {
                if (hardcard && answerIndex == i)
                    randomcard = generateHardCard();
                else
                {
                    if (Random.Range(0, 100) <= 50)
                        randomcard = generateMediumCard();
                    else
                        randomcard = generateEasyCard();
                }
                while (choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
                {
                    if (hardcard && answerIndex == i)
                        randomcard = generateHardCard();
                    else
                    {
                        if (Random.Range(0, 100) <= 50)
                            randomcard = generateMediumCard();
                        else
                            randomcard = generateEasyCard();
                    }
                }
            }
            randomindex = deck.IndexOf(randomcard);
            posindeck.Add(randomindex);
            choices.Add(randomcard);
            GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(cspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomcard.title;
            go.SetActive(true);
            if (i == answerIndex)
            {
                GameObject an = Instantiate(afab, new Vector3(0, 0, 0), Quaternion.identity);
                an.transform.SetParent(aspace);
                an.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomcard.getInfoLine(1);
                an.SetActive(true);
            }
        }
    }
    public bool hasDuplicateAnswer(Card c)
    {
        foreach (Card i in choices)
        {
            if (c.getInfoLine(1) == i.getInfoLine(1))
            {
                return true;
            }
        }
        return false;
    }
    Card generateNewCard()
    {
        List<Card> cards = deck.getNewCards();
        if (cards.Count > 0)
        {
            Card c = cards[Random.Range(0, cards.Count)];
            if (choices.Contains(c) || hasDuplicateAnswer(c))
                if (cards.Count > 4)
                    return generateNewCard();
                else
                    if (Random.Range(0, 100) <= 75)
                        return generateMediumCard();
                    else
                        return generateHardCard();
            else
                return c;
        }
        else
            if (Random.Range(0, 100) <= 75)
            return generateMediumCard();
        else
            return generateHardCard();
    }

    Card generateEasyCard()
    {
        List<Card> cards = deck.getMatureCards();
        if (cards.Count > 0)
        {
            Card c = cards[Random.Range(0, cards.Count)];
            if (choices.Contains(c) || hasDuplicateAnswer(c))
                if (Random.Range(0, 100) <= 75)
                    return generateMediumCard();
                else
                    return generateHardCard();
            else
                return c;
        }
        else
            if (Random.Range(0, 100) <= 75)
                return generateMediumCard();
            else
                return generateHardCard();
    }
    Card generateMediumCard()
    {
        List<Card> cards = deck.getMediumCards();
        if (cards.Count > 0)
        {
            Card c = cards[Random.Range(0, cards.Count)];
            if (choices.Contains(c) || hasDuplicateAnswer(c))
                if (Random.Range(0, 100) <= 50)
                    return generateEasyCard();
                else
                    return generateHardCard();
            else
                return c;
        }
        else
            if (Random.Range(0, 100) <= 50)
                return generateEasyCard();
            else
                return generateHardCard();
    }
    Card generateHardCard()
    {
        List<Card> cards = deck.getHardCards();
        if (cards.Count > 0)
        {
            Card c = cards[Random.Range(0, cards.Count)];
            if (choices.Contains(c) || hasDuplicateAnswer(c))
                if (Random.Range(0, 100) <= 25)
                    return generateEasyCard();
                else
                    return generateMediumCard();
            else
                return c;
        }
        else
            if (Random.Range(0, 100) <= 25)
                return generateEasyCard();
            else
                return generateMediumCard();
    }
    public void submitAnswer(Transform t)
    {
        int tindex = t.transform.GetSiblingIndex();
        if (answerIndex == tindex)
        {
            gc.RightAnswer();
            gd.deck.incrementCorrect(posindeck[answerIndex]);
            rightanswertime = Time.time;
            gd.deck.updateAverageCardTime(posindeck[answerIndex], rightanswertime - lastrightanswertime);
            generateRound();
        }
        else
        {
            gc.WrongAnswer();
            gd.deck.incrementWrong(posindeck[answerIndex]);
            gd.deck.incrementWrong(posindeck[tindex]);
        }
        gd.saveDeck();
    }
}
