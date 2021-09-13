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
    public GameObject multiplechoicefab;
    public Transform gamefield;
    // Start is called before the first frame update
    void Awake()
    {
        deck = loadDeck(System.IO.Directory.GetFiles("./Decks/Temp/")[0]);
        GameObject go = Instantiate(multiplechoicefab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(gamefield);
        go.SetActive(true);
        go.GetComponent<MultipleChoice>().deck = deck;
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
