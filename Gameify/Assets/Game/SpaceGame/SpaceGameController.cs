using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGameController : GameController
{
    public GameObject cam;
    public GameObject missile;
    public GameObject playership;
    public GameObject playershipfiringpoint;
    public GameObject enemyship;
    public GameObject enemyshipfiringpoint;

    void Start()
    {
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
        /*
        GameObject go = Instantiate(explosion, new Vector3(enemyship.transform.position.x, enemyship.transform.position.y, 0), Quaternion.identity);
        go.transform.SetParent(enemyship.transform);
        go.transform.Rotate(new Vector3(180, 0, 0));
        go.transform.localPosition = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0f);
        float ranscale = Random.Range(0.2f, 0.75f);
        go.transform.localScale = new Vector3(ranscale, ranscale, 0);*/
    }
    public override void WrongAnswer()
    {
        GameObject go = Instantiate(missile, enemyshipfiringpoint.transform.position, Quaternion.identity);
        go.transform.SetParent(enemyshipfiringpoint.transform);
        go.GetComponent<Bullet>().endpoint = playership;
        go.SetActive(true);
        /*
        GameObject go = Instantiate(explosion, new Vector3(playership.transform.position.x, playership.transform.position.y, 0), Quaternion.identity);
        go.transform.SetParent(playership.transform);
        go.transform.Rotate(new Vector3(180, 0, 0));
        go.transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        float ranscale = Random.Range(0.2f, 0.3f);
        go.transform.localScale = new Vector3(ranscale, ranscale, 0);
        */
    }
}

