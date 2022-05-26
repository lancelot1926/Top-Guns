using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootingCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject goldBulletMid;
    private PlayerMovement pMove;
    public float bulletForceMid = 20f;
    public Vector3 shoot;
    public Vector3 shootirection;
    public float angle;
    public GameObject deadZone;
    public int shootingStyleCount;
    private float nextShootTime;
    private float bcdr;
    private bool bursting;

    private void Awake()
    {
        pMove = gameObject.GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootingStyleCount++;
            if (shootingStyleCount >= 5)
            {
                shootingStyleCount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            shootingStyleCount--;
            if (shootingStyleCount < 0)
            {
                shootingStyleCount = 4;
            }
        }
        switch (shootingStyleCount)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot();
                    

                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Time.time > nextShootTime&&!bursting)
                    {

                        bursting = true;
                        StartCoroutine(BurstFire());
                        
                        
                        float fireRate = 0.5f;
                        nextShootTime = Time.time + fireRate;
                    }


                }
                break;
            case 2:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.1F;
                        nextShootTime = Time.time + fireRate;
                    }


                }
                break;
            case 3:

                break;
            case 4:

                break;
        }
        //Vector2.Distance(pMove.mousePos, deadZone.GetComponent<CircleCollider2D>().bounds.center) < deadZone.GetComponent<CircleCollider2D>().radius
        
    }
    private void FixedUpdate()
    {
        

        shootirection=(pMove.mousePos - firePoint.position).normalized;
        angle = Mathf.Atan2(shootirection.y, shootirection.x) * Mathf.Rad2Deg;


    }

    void Shoot()
    {
        GameObject bullet = Instantiate(goldBulletMid, firePoint.position, Quaternion.identity/*firePoint.rotation */);
        shootirection.z = 0;
        bullet.GetComponent<Shoot>().Setup(shootirection,angle,gameObject);
        
        
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForceMid, ForceMode2D.Impulse);

    }
    IEnumerator BurstFire()
    {
        yield return new WaitForSeconds(0.08f);
        Shoot();
        yield return new WaitForSeconds(0.08f);
        Shoot();
        yield return new WaitForSeconds(0.08f);
        Shoot();
        bursting = false;

    }

    IEnumerator EventDelayer(float delay, Action onActionComplete)
    {
        yield return new WaitForSeconds(delay);
        onActionComplete();
    }
}
