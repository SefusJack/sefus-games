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
    public int displayedscore = 0;
    public float lastrightanswertime = 0;
    public int score = 0;
    public int addedscore = 0;
    public int combo = 0;
    
    public bool incrementscorerunning = false;
    public bool addscorefaderunning = false;


    public Slider healthslider;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI scoreaddtext;
    public TextMeshProUGUI combotext;

    void Start()
    {
        scoretext.text = "Score: " + score.ToString("D6");
        scoreaddtext.text = "";
        combotext.text = "Combo: " + combo;
    }
    void Update()
    {
        cam.transform.Translate(new Vector3(0, 1f * Time.deltaTime, 0));
    }
    public override void RightAnswer()
    {
        lastrightanswertime = Time.time;
        GameObject go = Instantiate(missile, playershipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(playershipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = enemyship;
        go.SetActive(true);

        score = score + 100;
        addedscore = addedscore + 100;
        scoreaddtext.text = "+" + addedscore;
        if(!incrementscorerunning)
            StartCoroutine("incrementScore");
        if(!addscorefaderunning){
            StartCoroutine("addedScoreFade");
        }
        combo++;
        updateCombo();
        StartCoroutine(damageFlash(enemyship));
    }
    public override void WrongAnswer()
    {
        GameObject go = Instantiate(missile, enemyshipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(enemyshipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = playership;
        go.SetActive(true);
        combo = 0;
        updateCombo();
        StartCoroutine(damageFlash(playership));
        health -= 10;
        healthslider.value = health;
    }
    public void updateCombo()
    {
        combotext.text = "Combo: " + combo;
    }
    IEnumerator incrementScore()
    {
        incrementscorerunning = true;
        while(displayedscore <= score)
        {
            yield return new WaitForSeconds(0.03f);
            scoretext.text = "Score: " + displayedscore.ToString("D6");
            displayedscore += 5;
        }
        if(displayedscore > score)
        {
            scoretext.text = "Score: " + score.ToString("D6");
            displayedscore = score;
        }
        incrementscorerunning = false;
    }
    IEnumerator addedScoreFade()
    {
        addscorefaderunning = true;
        scoreaddtext.color = new Color(scoreaddtext.color.r, scoreaddtext.color.g, scoreaddtext.color.b, 1f);
        while(lastrightanswertime+3f >= Time.time)
        {
            yield return new WaitForSeconds(3f);
        }
        while (scoreaddtext.color.a > 0.0)
        {
            scoreaddtext.color = new Color(scoreaddtext.color.r, scoreaddtext.color.g, scoreaddtext.color.b, scoreaddtext.color.a - (Time.deltaTime * 2f));
            yield return null;
        }
        addedscore = 0;
        addscorefaderunning = false;
    }
    IEnumerator damageFlash(GameObject sh)
    {
        float test = Time.deltaTime;
        SpriteRenderer s = sh.GetComponent<SpriteRenderer>();
        while(test <= 2f)
        {
            if(test > 0.5f && test < 0.8f)
            {
                s.color = Color.red;
            }
            if(test > 0.8f && test < 1.1f)
            {
               s.color = Color.white; 
            }
            if(test > 1.1f && test < 1.4f)
            {
               s.color = Color.red; 
            }
            if(test > 1.4f && test < 1.7f)
            {
               s.color = Color.white; 
            }
            test += Time.deltaTime;
            yield return null;
        }
    }
}

