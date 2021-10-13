using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSelection : MonoBehaviour
{
    public Decks decks;
    public GameObject gfab;
    public Transform gspace;
    List<string> games;
    void Start()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        games = new List<string>();
        for (int i = 1; i < sceneCount; i++)
        {
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i)));
            games.Add(System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i)));
        }
        displayGames();
    }
    public void displayGames()
    {
        for (int i = 0; i < games.Count; i++)
        {
            GameObject go = Instantiate(gfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(gspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = games[i];
            go.SetActive(true);
        }
    }
    public void selectScene(int index)
    {
        string[] files = System.IO.Directory.GetFiles("./Decks/Temp/");
        foreach (string filestr in files)
        {
            File.Delete(filestr);
        }
        string destination = "./Decks/Temp/" + decks.curdeck.title + ".dat";
        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, decks.curdeck);
        file.Close();

        SceneManager.LoadScene(games[index], LoadSceneMode.Single);
    }
}
