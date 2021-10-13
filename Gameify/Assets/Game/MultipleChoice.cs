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

    public float rightanswertime = 0;
    public float lastrightanswertime = 0;
    
    public void Start()
    {
        deck = gd.GetComponent<GameDeck>().deck;
        generateCampaignRound();
    }
    public void generateRandomRound()
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
        for (int i = 0; i < 4; i++)
        {
            generateRandomCard(i);
        }
    }
    public void generateRandomCard(int i)
    {
        int randomindex = Random.Range(0, deck.Count);
        Card randomcard = deck.getCard(randomindex);
        while (choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
        {
            randomindex = Random.Range(0, deck.Count);
            randomcard = deck.getCard(randomindex);
        }
        posindeck.Add(randomindex);
        choices.Add(randomcard);
        InstantiateCard(randomcard, i);
    }
    public void generateCampaignRound()
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
        for (int i = 0; i < 4; i++)
        {
            if(deck.newcards.Count != 0)
            {
                generateCampaignCard(i, deck.newcards);
            }
            else if(deck.youngcards.Count != 0)
            {
                if(deck.youngcards.Count < deck.Count*0.1)
                {
                    int chance = Random.Range(0, 100);
                    if(chance > 15)
                        generateCampaignCard(i, deck.maturecards);
                    else
                       generateCampaignCard(i, deck.youngcards); 
                }
                else
                    generateCampaignCard(i, deck.youngcards);
            }
            else if(deck.maturecards.Count != 0)
            {
                generateCampaignCard(i, deck.maturecards);
            }
        }
    }
    public void generateCampaignCard(int i, List<int> subdeck)
    {
        int randomindex;
        Card randomcard;
        if(subdeck.Count > 4)
        {
            randomindex = Random.Range(0, subdeck.Count);
            randomcard = deck.getCard(subdeck[randomindex]);
            while (choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
            {
                randomindex = Random.Range(0, subdeck.Count);
                randomcard = deck.getCard(subdeck[randomindex]);
            }
        }
        else
        {
            if(i == answerIndex)
            {
                randomindex = Random.Range(0, subdeck.Count);
                randomcard = deck.getCard(subdeck[randomindex]);
            }
            else
            {
                randomindex = Random.Range(0, deck.Count);
                randomcard = deck.getCard(randomindex);
                while (subdeck.Contains(randomindex) || choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
                {
                    randomindex = Random.Range(0, deck.Count);
                    randomcard = deck.getCard(randomindex);
                }
                posindeck.Add(randomindex);
                choices.Add(randomcard);
                InstantiateCard(randomcard, i);
                return;
            }
        }
        posindeck.Add(subdeck[randomindex]);
        choices.Add(randomcard);
        InstantiateCard(randomcard, i);
    }
    public void InstantiateCard(Card rc, int i)
    {
        GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(cspace);
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rc.title;
        go.SetActive(true);
        if (i == answerIndex)
        {
            GameObject an = Instantiate(afab, new Vector3(0, 0, 0), Quaternion.identity);
            an.transform.SetParent(aspace);
            an.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rc.getInfoLine(1);
            an.SetActive(true);
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
    public void submitAnswer(Transform t)
    {
        int tindex = t.transform.GetSiblingIndex();
        if (answerIndex == tindex)
        {
            gc.RightAnswer();
            Debug.Log(posindeck[answerIndex]);
            gd.deck.incrementCorrect(posindeck[answerIndex]);
            rightanswertime = Time.time;
            gd.deck.updateAverageCardTime(posindeck[answerIndex], rightanswertime - lastrightanswertime);
            generateCampaignRound();
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
