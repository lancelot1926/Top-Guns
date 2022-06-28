using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 shootDir;
    private string ownertag;
    private GameObject owner;

    [SerializeField]
    private GameObject hitEffect;
    public void Setup(Vector3 shootDir,float ang,GameObject Owner,int shotMode) {
        this.shootDir = shootDir;
        owner = Owner;
        this.ownertag = Owner.tag;
        if (ownertag == "Player")
        {
            if (shotMode != 3)
            {
                transform.eulerAngles = new Vector3(-ang, 90, 0);
            }
            if (shotMode == 3)
            {
                transform.eulerAngles = new Vector3(-ang, 90, 0);
                //transform.eulerAngles = new Vector3(0, 0, ang);
            }
        }
        if (ownertag == "Enemy")
        {
            transform.eulerAngles = new Vector3(-ang, 90, 0);
        }


        //gameObject.GetComponent<Rigidbody2D>().rotation = ang;
    }


    private void Update()
    {
        float moveSpeed = 50f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag != ownertag&&collision.tag!=gameObject.tag&&collision.tag!="Item")
        {
            if (collision.tag == "Enemy") 
            {
                Instantiate(hitEffect, collision.transform);
                collision.GetComponent<UnitStats>().TakeDamage(10);
                if (owner.GetComponent<UnitStats>().hasBloodLust==true)
                {
                    owner.GetComponent<UnitStats>().RecieveHealth(3);
                }
                
            }
            if (collision.tag == "Player")
            {
                Instantiate(hitEffect, collision.transform);

                collision.GetComponent<UnitStats>().TakeDamage(10);
            }
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
