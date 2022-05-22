using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir,float ang) {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(-ang, 90, 0);
        //gameObject.GetComponent<Rigidbody2D>().rotation = ang;
    }


    private void Update()
    {
        float moveSpeed = 21f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
