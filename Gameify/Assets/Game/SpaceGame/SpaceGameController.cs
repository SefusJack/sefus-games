using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceGameController : GameController
{
    public GameObject cam;
    public GameObject missile;
    public GameObject playership;
    public GameObject playershipfiringpoint;
    public GameObject enemyship;
    public GameObject enemyshipfiringpoint;


    public int health = 100;
    public int score = 0;
    public int combo = 0;


    public Slider healthslider;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI combotext;

    void Start()
    {
        scoretext.text = "Score: " + score.ToString("D6");
        combotext.text = "Combo: " + combo;
    }
    void Update()
    {
        cam.transform.Translate(new Vector3(0, 1f * Time.deltaTime, 0));
    }
    public override void RightAnswer()
    {
        GameObject go = Instantiate(missile, playershipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(playershipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = enemyship;
        go.SetActive(true);

        score = score + 100;
        updateScore();
        combo++;
        updateCombo();
    }
    public override void WrongAnswer()
    {
        GameObject go = Instantiate(missile, enemyshipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(enemyshipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = playership;
        go.SetActive(true);
        combo = 0;
        updateCombo();
    }
    public void updateScore()
    {
        scoretext.text = "Score: " + score.ToString("D6");
    }
    public void updateCombo()
    {
        combotext.text = "Combo: " + combo;
    }
}

