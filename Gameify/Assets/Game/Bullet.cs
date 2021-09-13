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
    }
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, 0.01f);
        transform.rotation = Quaternion.LookRotation(endpoint.transform.position, Vector3.back);
        Debug.Log(transform.rotation);
        if (transform.position == endpoint.transform.position)
        {
            Effect();
            Destroy(gameObject);
        }
    }
    public void Effect()
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        go.transform.Rotate(new Vector3(180, 0, 0));
    }
}
