using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public  Animator    mageAnimator; //Adicionando o animator do gameobject sprite (filho)

    private Rigidbody2D mageRigidbody2D;
    public  float       moveSpeed;
    private float       touchRun = 0.0f;

    public  Transform   groundCheck;
    public  bool        facingRight = true;
    public  bool        isGround = false;

    public  GameObject  sprite; // Pegando o gameobject filho sprite, gameobject dos sprites e anima��es

    public  float       jumpForce;
    private int         numberJumps;
    public  int         maxJumps;

    private bool isJumping = false;
    private bool isFalling = false;
    private bool isLanding = false;

    private float tempo;
    public  float delayAnimation;

    

    // Start is called before the first frame update
    void Start()
    {
        
        mageRigidbody2D = GetComponent<Rigidbody2D>(); //Adicionando Rigidbody a vari�vel
        sprite = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); //Se tocar em algum tile colider, o isGround vira true
        mageAnimator.SetBool("IsGrounded", isGround); //Passando o true do isGround para o IsGrounded do animator

        touchRun = Input.GetAxisRaw("Horizontal");  
        MoveMage();

        SetAnimations();

       
        Debug.Log(tempo);
        Debug.Log(delayAnimation);


        if (touchRun < 0 && facingRight || touchRun > 0 && !facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpMage();
            isJumping = true;
        }
        
    }

    void MoveMage()
    {
        mageRigidbody2D.velocity = new Vector2(moveSpeed * touchRun, mageRigidbody2D.velocity.y);
        //mageAnimator.SetBool("Running", mageRigidbody2D.velocity.x != 0);
    }

    void Flip()
    {
        facingRight = !facingRight;
        
        sprite.transform.localScale = new Vector3(-sprite.transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void JumpMage()
    {
        if (isGround)
        {
            numberJumps = 0;
        }

        
        if (numberJumps < maxJumps)
        {
            mageRigidbody2D.AddForce(new Vector2(0f, jumpForce));
            isGround = false;
            numberJumps++;
        }
        isJumping = false;        
    }

    void SetAnimations()
    {
        mageAnimator.SetFloat("EixoY", mageRigidbody2D.velocity.y);
        mageAnimator.SetBool("Running", mageRigidbody2D.velocity.x != 0 && isGround);
        mageAnimator.SetBool("IsJumping", !isGround);
    }

}
