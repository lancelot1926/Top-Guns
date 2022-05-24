using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierAi : MonoBehaviour
{
    public float MoveSpeed;
    public float verticalMove;
    public float horizontalMove;
    private Vector3 moveDirection;
    [SerializeField]
    private Animator anim;
    public Vector3 StartPoint;
    public Vector3 RoamPosiiton;
    private GameObject Player;
    private State state;


    public Transform firePoint;
    public GameObject goldBulletMid;
    public float bulletForceMid = 20f;
    public Vector3 shoot;
    public Vector3 shootirection;
    public float angle;
    private bool tookAShot;
    private float nextShootTime;

    private enum State
    {
        Roaming,
        Chasing,
        Returning,
        Shooting,
    }
    void Start()
    {
        
        StartPoint = transform.position;
        RoamPosiiton = GetRoamingPoisition();
        Player = GameObject.FindGameObjectWithTag("Player");
        state = State.Roaming;

    }

    private void Update()
    {
        Debug.Log(state);
    }

    private void FixedUpdate()
    {
        shootirection = (Player.transform.position - firePoint.position).normalized;
        angle = Mathf.Atan2(shootirection.y, shootirection.x) * Mathf.Rad2Deg;
        switch (state)
        {
            default:
            case State.Roaming:
                transform.position += (RoamPosiiton - transform.position) * MoveSpeed * Time.fixedDeltaTime;
                anim.SetBool("Walking", true);
                anim.SetFloat("XMovement", moveDirection.x);
                anim.SetFloat("YMovement", moveDirection.y);
                if (Vector3.Distance(transform.position, RoamPosiiton) < 1f)
                {
                    anim.SetBool("Walking", false);

                    RoamPosiiton = GetRoamingPoisition();
                    /*StartCoroutine(EventDelayer(3f, () => {

                    }));*/
                }

                FindTarget();
                break;
            case State.Chasing:
                transform.position += (Player.transform.position - transform.position) * MoveSpeed * Time.fixedDeltaTime;
                anim.SetBool("Walking", true);
                anim.SetFloat("XMovement", Player.GetComponent<PlayerMovement>().moveX);
                anim.SetFloat("YMovement", Player.GetComponent<PlayerMovement>().moveY);

                float aimDistance = 15f;
                if(Vector3.Distance(transform.position, Player.transform.position) <aimDistance)
                {
                    anim.SetBool("Walking", false);
                    state = State.Shooting;
                }

                float stopChasingDistance = 30;
                if (Vector3.Distance(transform.position, Player.transform.position) > stopChasingDistance)
                {
                    state = State.Returning;
                }
                break;
            case State.Shooting:
                anim.SetBool("Shooting", true);
                anim.SetFloat("CursorX", shootirection.x);
                anim.SetFloat("CursorY", shootirection.y);
                if (Time.time > nextShootTime)
                {
                    

                    Shoot();
                    float fireRate = 0.3f;
                    nextShootTime = Time.time + fireRate;
                }
                float aimD = 15f;
                if (Vector3.Distance(transform.position, Player.transform.position) > aimD)
                {
                    anim.SetBool("Shooting", false);
                    state = State.Chasing;
                }


                break;
            case State.Returning:
                transform.position += (StartPoint - transform.position) * MoveSpeed * Time.fixedDeltaTime;
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                anim.SetBool("Walking", true);
                anim.SetFloat("XMovement", StartPoint.x);
                anim.SetFloat("YMovement", StartPoint.y);
                FindTarget();
                if (Vector3.Distance(transform.position, StartPoint) < 1f)
                {
                    gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    state = State.Roaming;
                }
                break;

        }

    }
    void Shoot()
    {
        
        GameObject bullet = Instantiate(goldBulletMid, firePoint.position, Quaternion.identity/*firePoint.rotation */);
        shootirection.z = 0;
        bullet.GetComponent<Shoot>().Setup(shootirection, angle,gameObject.tag);


        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForceMid, ForceMode2D.Impulse);

    }
    private Vector3 GetRoamingPoisition()
    {
        return StartPoint + GetRandomDir() * UnityEngine.Random.Range(5f, 5f);
    }

    private Vector3 GetRandomDir()
    {
        moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

        return moveDirection;
    }

    private void FindTarget()
    {
        float targetRange = 20f;
        if (Vector3.Distance(transform.position, Player.transform.position) < targetRange)
        {
            state = State.Chasing;
        }
        float aimDistance = 15f;
        if (Vector3.Distance(transform.position, Player.transform.position) < aimDistance)
        {
            anim.SetBool("Walking", false);
            state = State.Shooting;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {



    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //RoamPosiiton = GetRoamingPoisition();
    }

    IEnumerator EventDelayer(float delay, Action onActionComplete)
    {
        yield return new WaitForSeconds(delay);
        onActionComplete();
    }
}
