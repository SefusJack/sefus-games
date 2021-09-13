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
    public GameDeck gdeck;
    public Deck deck;
    public List<Card> choices;
    public int answerIndex = 0;
    public GameObject cfab;
    public GameObject afab;
    public Transform qspace;
    public Transform cspace;
    public Transform aspace;
    public void Start()
    {
        generateRound();
    }
    public void generateRound()
    {
        choices = new List<Card>();
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
            Card randomcard = deck.getCard(Random.Range(0, deck.Count));
            while (choices.Contains(randomcard) || hasDuplicateAnswer(randomcard))
            {
                randomcard = deck.getCard(Random.Range(0, deck.Count));
            }
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
    public void submitAnswer(Transform t)
    {
        if (answerIndex == t.transform.GetSiblingIndex())
        {
            generateRound();
            gc.RightAnswer();
        }
        else
        {
            gc.WrongAnswer();
        }
    }
}
