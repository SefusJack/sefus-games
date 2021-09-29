using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDeck : MonoBehaviour
{
    public Deck deck = new Deck("");
    // Start is called before the first frame update
    void Start()
    {
        deck = loadDeck(System.IO.Directory.GetFiles("./Decks/Temp/")[0]);
        //go.GetComponent<MultipleChoice>().deck = deck;
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
    public void saveDeck()
    {
        string destination = System.IO.Directory.GetFiles("./Decks/Temp/")[0];
        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, deck);
        file.Close();
    }
}
