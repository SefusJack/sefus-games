using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceGameController : GameController
{
    public GameObject cam;
    public GameObject missile;
    public GameObject scoreaddfab;
    public GameObject scoreaddspace;
    public GameObject playership;
    public GameObject playershipfiringpoint;
    public GameObject enemyship;
    public GameObject enemyshipfiringpoint;


    public int health = 100;
    public int displayedscore = 0;
    public float lastrightanswertime = 0;
    public int score = 0;
    public int combo = 0;
    
    public bool incrementscorerunning = false;


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
        lastrightanswertime = Time.time;
        GameObject go = Instantiate(missile, playershipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(playershipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = enemyship;
        go.SetActive(true);

        float ranx = Random.Range(-20f, 20f);
        float rany = Random.Range(-10f, 10f); 
        GameObject add = Instantiate(scoreaddfab, scoreaddspace.transform.position, Quaternion.identity);
        add.transform.SetParent(scoreaddspace.transform);
        add.transform.localPosition = new Vector3(ranx, rany, 0f);
        add.SetActive(true);

        score = score + 100;
        if(!incrementscorerunning)
            StartCoroutine("incrementScore");

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

