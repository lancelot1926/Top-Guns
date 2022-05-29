using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

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


    private Seeker seeker;
    private Rigidbody2D rb;
    

    public float speed = 400f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint;
    bool reachedEndOfPath = false;

    private enum State
    {
        Roaming,
        Chasing,
        Returning,
        Shooting,
    }
    void Start()
    {
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        StartPoint = transform.position;
        RoamPosiiton = GetRoamingPoisition();
        Player = GameObject.FindGameObjectWithTag("Player");
        state = State.Roaming;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }
    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, Player.transform.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void Update()
    {
        Debug.DrawLine(transform.position, Player.transform.position, Color.red);
        //Debug.Log(Physics2D.Linecast(transform.position, Player.transform.position).transform.tag);
        //Debug.Log(state);
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        RaycastHit2D rayHit = Physics2D.Linecast(firePoint.position, Player.transform.position, 1 << LayerMask.NameToLayer("Obstacle"));
        //Debug.Log(rayHit.collider.gameObject.tag);
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
                transform.position += ((Vector3)path.vectorPath[currentWaypoint] - transform.position).normalized * 5 * Time.fixedDeltaTime;
                anim.SetBool("Walking", true);
                anim.SetFloat("XMovement", Player.GetComponent<PlayerMovement>().moveX);
                anim.SetFloat("YMovement", Player.GetComponent<PlayerMovement>().moveY);
                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
                

                float aimDistance = 30F;
                if(Vector3.Distance(transform.position, Player.transform.position) <aimDistance&& rayHit.collider == null)
                {
                    
                    anim.SetBool("Walking", false);
                    state = State.Shooting;
                }

                float stopChasingDistance = 100F;
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
                    float fireRate = 1F;
                    nextShootTime = Time.time + fireRate;
                }
                /*if(Physics2D.Linecast(transform.position, Player.transform.position))
                {
                    anim.SetBool("Shooting", false);
                    state = State.Chasing;
                }*/

                if (rayHit.collider != null)
                {
                    if (rayHit.collider.gameObject.CompareTag("Envi"))
                    {
                        anim.SetBool("Shooting", false);
                        state = State.Chasing;
                    }
                }
                float aimD = 30F;
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
        bullet.GetComponent<Shoot>().Setup(shootirection, angle,gameObject,0);


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
        float targetRange = 80F;
        if (Vector3.Distance(transform.position, Player.transform.position) < targetRange)
        {
            state = State.Chasing;
        }
        /*float aimDistance = 30F;
        if (Vector3.Distance(transform.position, Player.transform.position) < aimDistance)
        {
            anim.SetBool("Walking", false);
            state = State.Shooting;
        }*/

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
