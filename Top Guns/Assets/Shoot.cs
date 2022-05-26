using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 shootDir;
    private string ownertag;
    private GameObject owner;
    public void Setup(Vector3 shootDir,float ang,GameObject owner) {
        this.shootDir = shootDir;
        this.ownertag = owner.tag;
        transform.eulerAngles = new Vector3(-ang, 90, 0);
        //gameObject.GetComponent<Rigidbody2D>().rotation = ang;
    }


    private void Update()
    {
        float moveSpeed = 21f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag != ownertag&&collision.tag!=gameObject.tag)
        {
            if (collision.tag == "Enemy") 
            {
                collision.GetComponent<UnitStats>().TakeDamage(20);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
