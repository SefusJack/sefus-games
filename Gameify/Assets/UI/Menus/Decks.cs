using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decks : MonoBehaviour
{
    List<Deck> decks;


    public Deck curdeck;
    int curdeckpos;
    Card curcard;
    int curcardpos;

    public TMP_InputField dtitlefield;
    public TextMeshProUGUI dtitle;
    public GameObject dfab;
    public Transform dspace;
    public GameObject cfab;
    public Transform cspace;
    public GameObject cinfo;

    public bool newDeck = false;

    public void Start()
    {
        decks = new List<Deck>();
    }

    public void addDeck()
    {
        newDeck = true;
        decks.Add(new Deck(""));
        curdeckpos = decks.Count - 1;
        curdeck = decks[curdeckpos];
        GameObject go = Instantiate(dfab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(dspace);
        go.SetActive(true);
    }
    public void addCard()
    {
        curdeck.Add(new Card());
        GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(cspace);
        go.SetActive(true);
    }
    public void selectDeck(int index)
    {
        newDeck = false;
        curdeckpos = index;
        curdeck = decks[index];
        dtitlefield.text = curdeck.title;
        dtitle.text = curdeck.title;
        displayCards();
    }
    public void setDeckTitle()
    {
        if(dtitlefield.text != curdeck.title)
        {
            curdeck.title = dtitlefield.text;
            dtitle.text = dtitlefield.text;
            displayDecks();
        }
    }
    public bool hasDeckTitle(Deck d, string s)
    {
        foreach (Deck dtmp in decks)
        {
            if (d != dtmp)
            {
                if (s == dtmp.title)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void selectCard(int index)
    {
        curcardpos = index;
        curcard = curdeck.getCard(index);
        int i = 0;
        foreach (TMP_InputField field in cinfo.GetComponentsInChildren<TMP_InputField>())
        {
            field.text = curcard.getInfoLine(i);
            i++;
        }
    }
    public void editCurrentCard()
    {
        List<string> info = new List<string>();
        foreach (TMP_InputField field in cinfo.GetComponentsInChildren<TMP_InputField>())
        {
            info.Add(field.text);
        }
        curcard = new Card(info);
        cspace.GetChild(curcardpos).GetChild(0).GetComponent<TextMeshProUGUI>().text = curcard.title;
        curdeck.setCard(curcard, curcardpos);
    }
    public void displayDecks()
    {
        foreach (Transform child in dspace)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < decks.Count; i++)
        {
            GameObject go = Instantiate(dfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(dspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = decks[i].title;
            go.SetActive(true);
        }
    }
    public void displayCards()
    {
        foreach(Transform child in cspace)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < curdeck.Count; i++)
        {
            GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(cspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curdeck.getCard(i).title;
            go.SetActive(true);
        }
    }
    public void Remove(Deck d)
    {
        decks.Remove(d);
    }
}

