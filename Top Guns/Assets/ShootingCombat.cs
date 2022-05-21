using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject goldBulletMid;
    private PlayerMovement pMove;
    public float bulletForceMid = 20f;
    public Vector3 shoot;
    public Vector3 shootirection;
    public float angle;
    private void Awake()
    {
        pMove = gameObject.GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
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
        bullet.GetComponent<Shoot>().Setup(shootirection,angle);
        
        
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForceMid, ForceMode2D.Impulse);

    }
}
