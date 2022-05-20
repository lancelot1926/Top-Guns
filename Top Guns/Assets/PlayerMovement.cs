using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigBody;
    [SerializeField]
    private Animator animator;
    private Vector3 moveDir;

    private float moveSpeed = 80f;
    public float moveX;
    public float moveY;
    public float mouseX;
    public float mouseY;

    private void Awake()
    {
        
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = 0f;
        moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += new Vector3(0, 1);
            moveY = +1f;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += new Vector3(0, -1);
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(-1, 0);
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(1, 0);
            moveX = +1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;
        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            rigBody.velocity = Vector2.zero;
            animator.SetBool("Walking", false);
            //animator.SetFloat("XMovement", moveDir.x);
            //animator.SetFloat("YMovement", moveDir.y);
        }

        if (isIdle == false)
        {
            animator.SetBool("Walking", true);
            animator.SetFloat("XMovement", moveDir.x);
            animator.SetFloat("YMovement", moveDir.y);
        }
        
    }

    private void FixedUpdate()
    {
        rigBody.velocity = moveDir*moveSpeed;
    }
}
