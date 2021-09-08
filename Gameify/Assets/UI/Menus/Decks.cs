using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decks : MonoBehaviour
{
    List<Deck> decks;


    Deck curdeck;
    int curdeckpos;
    public TMP_InputField decktitle;
    Card curcard;
    int curcardpos;

    public void Start()
    {
        decks = new List<Deck>();
    }

    public GameObject dfab;
    public Transform dspace;
    public void addDeck()
    {
        decks.Add(new Deck());
        GameObject go = Instantiate(dfab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(dspace);
        go.SetActive(true);
    }
    public void setCurrentDeck(int index)
    {
        curdeckpos = index;
        curdeck = decks[index];
        decktitle.text = dspace.GetComponentsInChildren<TextMeshProUGUI>()[index].text;
        getCards();
    }
    public void getCards()
    {
        foreach(Transform child in cspace)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < curdeck.Count; i++)
        {
            GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(cspace);
            Debug.Log(curdeck.Get(i));
            Debug.Log(curdeck.Get(i).title);
            //go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curdeck.Get(i).title;
            go.SetActive(true);
        }
    }
    public void setDeckTitle()
    {
        dspace.GetComponentsInChildren<TextMeshProUGUI>()[curdeckpos].text = decktitle.text;
    }

    public GameObject cfab;
    public Transform cspace;
    public void addCard()
    {
        GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(cspace);
        go.SetActive(true);

        List <string> info = new List<string>();
        foreach (TextMeshProUGUI field in go.GetComponentsInChildren<TextMeshProUGUI>())
        {
            info.Add(field.text);
        }
        curdeck.Add(new Card(info));
        curdeck.Get(curcardpos).title = "Test";
    }
    public GameObject cinfo;
    public void setCurrentCard(int index)
    {
        curcardpos = index;
        curcard = curdeck.Get(index);
        int i = 0;
        foreach (TMP_InputField field in cinfo.GetComponentsInChildren<TMP_InputField>())
        {
            field.text = curcard.Get(i);
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
        curcard.title = info[0];
        curcard.SetInfo(info);
        cspace.GetChild(curcardpos).GetChild(0).GetComponent<TextMeshProUGUI>().text = curcard.title;
        curdeck.Set(curcard, curcardpos);
    }
}

