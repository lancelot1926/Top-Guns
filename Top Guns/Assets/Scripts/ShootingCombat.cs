using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootingCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject goldBulletMid;
    public GameObject shotgunBullet;
    private PlayerMovement pMove;
    public float bulletForceMid = 20f;
    public Vector3 shoot;
    public Vector3 shootirection;
    public float angle;
    public GameObject deadZone;
    public int shootingStyleCount;
    private float nextShootTime;
    public float fireRateCdr;
    private bool bursting;

    public List<string> shootingStyleList = new List<string> { "Rifle" };

    [SerializeField]
    private GameObject muzzleEffect;

    private void Awake()
    {
        pMove = gameObject.GetComponent<PlayerMovement>();
        fireRateCdr = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (shootingStyleList.Count > 1)
            {
                shootingStyleCount++;
                if (shootingStyleCount >= shootingStyleList.Count)
                {
                    shootingStyleCount = 0;
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (shootingStyleList.Count > 1)
            {
                shootingStyleCount--;
                if (shootingStyleCount < 0)
                {
                    shootingStyleCount = shootingStyleList.Count-1;
                }
            }
            
        }
        
        switch (shootingStyleList[shootingStyleCount])
        {
            case "Rifle":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        float fireRate = 0.3f-fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }


                }
                break;
            case "Burst":
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Time.time > nextShootTime&&!bursting)
                    {

                        bursting = true;
                        StartCoroutine(BurstFire());
                        
                        
                        float fireRate = 0.5f-fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }


                }
                break;
            case "Automatic":
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
            case "Shotgun":
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Time.time > nextShootTime)
                    {
                        ShootShotgun();
                        float fireRate = 0.8f-fireRateCdr;
                        nextShootTime = Time.time + fireRate;
                    }


                }
                break;
            case "Sniper":

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
        Instantiate(muzzleEffect, firePoint.transform);
        GameObject bullet = Instantiate(goldBulletMid, firePoint.position, Quaternion.identity/*firePoint.rotation */);
        
        shootirection.z = 0;
        bullet.GetComponent<Shoot>().Setup(shootirection,angle,gameObject,0);
        
        
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForceMid, ForceMode2D.Impulse);

    }
    void ShootShotgun()
    {
        Instantiate(muzzleEffect, firePoint.transform);
        GameObject bullet = Instantiate(shotgunBullet, firePoint.position, Quaternion.identity/*firePoint.rotation */);
        
        //shootirection.x = 0;
        bullet.GetComponent<Shoot>().Setup(shootirection, angle, gameObject,3);


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
