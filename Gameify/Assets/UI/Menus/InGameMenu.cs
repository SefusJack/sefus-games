using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    public GameObject decks;
    public void Start()
    {
        decks = GameObject.Find("Decks");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
