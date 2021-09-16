using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //0 fires towards an endpoint, 1 is forward until collision
    public GameObject explosion;
    public GameObject endpoint;
    public void Start()
    {
        float ranscale = Random.Range(0.3f, 1f);
        transform.localScale = new Vector3(ranscale, ranscale, 0);
    }
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, 0.1f);
        transform.rotation = Quaternion.Inverse(Quaternion.FromToRotation(endpoint.transform.position - transform.position, Vector3.up));
        if (transform.position == endpoint.transform.position)
        {
            Effect();
            Destroy(gameObject);
        }
    }
    public void Effect()
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        float ranscale = Random.Range(0.3f, 1f);
        go.transform.localScale = new Vector3(ranscale, ranscale, 0);
    }
}
