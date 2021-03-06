using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleMatchingEnhanced : GameController
{
    public GameObject cam;
    public Transform gamefield;
    public GameObject scoreaddfab;
    public GameObject scoreaddspace;
    public GameObject timebonusfab;
    public GameObject timebonusspace;
    public GameObject multiplechoicefab;


    public int health = 100;
    public int displayedscore = 0;
    public float rightanswertime = 0;
    public float lastrightanswertime = 0;
    public int score = 0;
    public int scoretoadd = 0;
    public int combo = 0;
    public int multiplier = 1;
    public int timebonus = 1;

    public bool incrementscorerunning = false;

    public Slider healthslider;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI combotext;
    public TextMeshProUGUI multipliertext;

    public AudioSource rightsound;
    public AudioSource wrongsound;
    public AudioSource multipliersound;

    void Start()
    {
        scoretext.text = "Score: " + score.ToString("D6");
        combotext.text = "Combo: " + combo;
        multipliertext.text = "x" + multiplier;

        GameObject go = Instantiate(multiplechoicefab, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(gamefield);
        go.SetActive(true);
    }
    void FixedUpdate()
    {
        cam.transform.Translate(new Vector3(0, 1f * Time.deltaTime, 0));
    }
    public override void RightAnswer()
    {
        lastrightanswertime = rightanswertime;
        rightanswertime = Time.time;

        timebonus = timeBonus();
        if (timebonus > 1)
            timeBonusPopup();
        scoretoadd = (100 * multiplier) * timebonus;
        scoreAddPopup();

        score = score + scoretoadd;
        if (!incrementscorerunning)
            StartCoroutine("incrementScore");
        

        combo++;
        updateCombo();

        rightsound.Play();
    }
    public override void WrongAnswer()
    {
        combo = 0;
        multiplier = 1;
        multipliertext.text = "x" + multiplier;
        updateCombo();
        health -= 10;
        healthslider.value = health;
        wrongsound.Play();
    }
    public void updateCombo()
    {
        combotext.text = "Combo: " + combo;
        switch(combo)
        {
            case 0:
                multiplier = 1;
                multipliertext.text = "x" + multiplier;
                multipliertext.color = Color.white;
                break;
            case 5:
                multiplier = 2;
                multipliertext.text = "x" + multiplier;
                multipliertext.color = Color.green;
                multipliersound.Play();
                break;
            case 10:
                multiplier = 3;
                multipliertext.text = "x" + multiplier;
                multipliertext.color = Color.blue;
                multipliersound.Play();
                break;
            case 20:
                multiplier = 4;
                multipliertext.text = "x" + multiplier;
                multipliertext.color = new Color(0.5f, 0.3f, 0.85f);
                multipliersound.Play();
                break;
            case 30:
                multiplier = 5;
                multipliertext.text = "x" + multiplier;
                multipliertext.color = new Color(1f, 0.6f, 0.0f);
                multipliersound.Play();
                break;
        }
    }
    public void scoreAddPopup()
    {
        float ranx = Random.Range(-20f, 20f);
        float rany = Random.Range(-10f, 10f);
        GameObject add = Instantiate(scoreaddfab, scoreaddspace.transform.position, Quaternion.identity);
        add.transform.SetParent(scoreaddspace.transform);
        add.transform.localPosition = new Vector3(ranx, rany, 0f);
        add.GetComponent<FadeTextMeshPro>().s = "+" + scoretoadd;
        add.SetActive(true);
    }
    public void timeBonusPopup()
    {
        GameObject tb = Instantiate(timebonusfab, timebonusspace.transform.position, Quaternion.identity);
        tb.transform.SetParent(timebonusspace.transform);
        tb.transform.localPosition = new Vector3(0f, 0f, 0f);
        tb.GetComponent<FadeTextMeshPro>().s = "TimeBonus : " + (rightanswertime - lastrightanswertime).ToString("F2") + " (x" + timebonus + ")";
        tb.SetActive(true);
    }
    IEnumerator incrementScore()
    {
        incrementscorerunning = true;
        while (displayedscore < score)
        {
            displayedscore = displayedscore + 1 + ((score - displayedscore) / 10);
            scoretext.text = "Score: " + displayedscore.ToString("D6");
            yield return new WaitForSeconds(0.05f);
        }
        if (displayedscore > score)
        {
            scoretext.text = "Score: " + score.ToString("D6");
            displayedscore = score;
        }
        incrementscorerunning = false;
    }
    public int timeBonus()
    {
        int reactiontime = Mathf.RoundToInt(rightanswertime - lastrightanswertime);
        switch (reactiontime)
        {
            case 1:
                return 6;
                break;
            case 2:
                return 5;
                break;
            case 3:
                return 4;
                break;
            case 4:
                return 3;
                break;
            case 5:
                return 2;
                break;
            default:
                return 1;
                break;
        }
    }
}

