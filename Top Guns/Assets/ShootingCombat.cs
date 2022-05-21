using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject goldBulletMid;
    private PlayerMovement pMove;
    public float bulletForceMid = 20f;

    private void Awake()
    {
        pMove = gameObject.GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(goldBulletMid, firePoint.position, Quaternion.identity/*firePoint.rotation */);
        bullet.GetComponent<Shoot>().Setup(pMove.lookDir,pMove.angle);
        
        
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForceMid, ForceMode2D.Impulse);

    }
}
