using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public GameObject dfabstate1;
    public GameObject dfabstate2;
    public Transform dspace;
    public GameObject cfab;
    public Transform cspace;
    public GameObject cinfo;

    public bool newDeck = false;

    public GameObject plusdeckbutton;

    //1 is Deck Manager, 2 is Deck Selector
    int state = 1;

    public void Start()
    {
        decks = new List<Deck>();
        string[] files = System.IO.Directory.GetFiles("./Decks/");
        foreach (string file in files)
        {
            if(Path.GetExtension(file) == ".dat")
            {
                addDeck(file);
            }
        }
    }
    public void setState(int st)
    {
        state = st;
        if(state == 1)
        {
            plusdeckbutton.SetActive(false);
        }
        else
        {
            plusdeckbutton.SetActive(true);
        }
        displayDecks();
    }
    public void addDeck()
    {
        newDeck = true;
        decks.Add(new Deck(""));
        curdeckpos = decks.Count - 1;
        curdeck = decks[curdeckpos];
        dtitlefield.text = "";
        GameObject go = Instantiate(dfabstate1, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(dspace);
        go.SetActive(true);
    }
    public void addDeck(Deck d)
    {
        newDeck = true;
        decks.Add(d);
        curdeckpos = decks.Count - 1;
        curdeck = decks[curdeckpos];
        dtitlefield.text = "";
        GameObject go = Instantiate(dfabstate1, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(dspace);
        go.SetActive(true);
    }
    public void addDeck(string filepath)
    {
        decks.Add(loadDeck(filepath));
    }
    public void addCard()
    {
        curdeck.Add(new Card());
        GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
        saveDeck();
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
            File.Delete("./Decks/" + curdeck.title + ".dat");
            curdeck.title = dtitlefield.text;
            dtitle.text = dtitlefield.text;
            displayDecks();
        }
        saveDeck();
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
        saveDeck();
    }
    public void displayDecks()
    {
        foreach (Transform child in dspace)
        {
            Destroy(child.gameObject);
        }
        if (state == 1)
        {
            for (int i = 0; i < decks.Count; i++)
            {
                GameObject go = Instantiate(dfabstate1, new Vector3(0, 0, 0), Quaternion.identity);
                go.transform.SetParent(dspace);
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = decks[i].title;
                go.SetActive(true);
                if (decks[i].Count <= 4)
                {
                    go.transform.GetComponent<Button>().interactable = false;
                }
            }
        }
        else if (state == 2)
        {
            for (int i = 0; i < decks.Count; i++)
            {
                GameObject go = Instantiate(dfabstate2, new Vector3(0, 0, 0), Quaternion.identity);
                go.transform.SetParent(dspace);
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = decks[i].title;
                go.SetActive(true);
            }
        }
    }
    public void displayCards()
    {   
        foreach (Transform child in cspace)
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
    public void RemoveDeck(Deck d)
    {
        decks.Remove(d);
    }
    public void RemoveCurrentDeck()
    {
        File.Delete("./Decks/" + curdeck.title + ".dat");
        decks.Remove(curdeck);
        dtitlefield.text = "";
        displayDecks();
    }
    public void RemoveCurrentCard()
    {
        curdeck.Remove(curcard);
        saveDeck();
        displayCards();
    }

    public void saveDeck()
    {
        string destination = "./Decks/" + curdeck.title + ".dat";
        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, curdeck);
        file.Close();
    }
    public Deck loadDeck(string filepath)
    {
        FileStream file;
        file = File.OpenRead(filepath);
        BinaryFormatter bf = new BinaryFormatter();
        Deck temp = (Deck)bf.Deserialize(file);
        file.Close();
        return temp;
    }
}

