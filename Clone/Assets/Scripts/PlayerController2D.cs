using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

    [Header("Move")]
    public float maxSpeed;
    [HideInInspector]
    public float trueMaxSpeed;

    [HideInInspector]
    public float currentSpeed;

    public float timeZeroToMax = 0;
    public float timeMaxToZero = 0;

    [HideInInspector]
    public float accelPerSec;
    [HideInInspector]
    public float decelPerSec;

    [HideInInspector]
    public float externalSpeed = 0;

    [HideInInspector]
    public float moveInput;

    [Header("Jump")]

    public float jumpForce;
    public int extraJumpValue;
    private int extraJumps;

    public bool holdToJump = true;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    [HideInInspector]
    public bool isGrounded;
    
    public Transform groundcheck;
    public Vector2 groundCheckSize;
    public LayerMask whatIsGround;

    [Space(10)]
    public Animator anim;

    [Space(10)]
    public bool isMoving = true;

    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public bool facingRight = true;

    [HideInInspector]
    public bool isDead;

    bool landed;

    //public ParticleSystem jumpParticle;
    public GameObject currentIdicator;
    public bool isCurrentSelected;

    void Awake () {
        accelPerSec = maxSpeed / timeZeroToMax;
        decelPerSec = -maxSpeed / timeMaxToZero;

        currentSpeed = 0;
        trueMaxSpeed = maxSpeed;

        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }


    public virtual void FixedUpdate () {
        if (!isDead) {
            isGrounded = Physics2D.OverlapBox(groundcheck.position, groundCheckSize, 0, whatIsGround);
            anim.SetBool("grounded", isGrounded);

            if (isMoving) {
                HorizontalMovement();
            }

            if (moveInput != 0) {
                Accelaration();
            } else {
                Decelaration();
            }
        }
               
    }

    public virtual void Update() {
        if (!isDead) {
            if (isMoving) {
                Jump();
            }
        }
    }

    public void HorizontalMovement() {

        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(currentSpeed + externalSpeed, rb.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(moveInput));

    }

    void Accelaration() {
        currentSpeed += accelPerSec * Time.deltaTime * moveInput;
        currentSpeed = Mathf.Clamp(currentSpeed,-maxSpeed, maxSpeed);
    }
    void Decelaration() {
        if(currentSpeed == 0) {
            return;
        }
        float reverseFactor = Mathf.Sign(currentSpeed);
        currentSpeed = Mathf.Abs(currentSpeed);
        currentSpeed += decelPerSec * Time.deltaTime;
        currentSpeed = Mathf.Max(currentSpeed,0) * reverseFactor;
    }

    public void SetCurrentSelected(bool value) {
        currentIdicator.SetActive(value);
        isCurrentSelected = value;
    }

    public void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void Jump() {
        if (isGrounded) {
            extraJumps = extraJumpValue;
            if (landed) {
                landed = false;
            }
        } else {
            landed = true;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0) {
            if(isCurrentSelected)
                MusicManager.instance.PlaySound("clean_short_jump_01");
            rb.velocity = new Vector2(currentSpeed + externalSpeed, jumpForce);
            extraJumps--;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        } 

        //Hold Jump
        if (holdToJump) {

            if (Input.GetButton("Jump") && isJumping) {
                if (jumpTimeCounter > 0) {
                    isJumping = true;
                    rb.velocity = new Vector2(currentSpeed + externalSpeed, jumpForce);                 
                    jumpTimeCounter -= Time.deltaTime;
                } else {
                    isJumping = false;
                }
            }
            if (Input.GetButtonUp("Jump")) {
                isJumping = false;
            }
        }
        
    }
    public void Kill() {
        LevelManager.instance.RemovePlayer(gameObject);
    }

    public virtual void Died() {
        isMoving = false;
        isDead = true;
        anim.SetBool("isDead", true);
        rb.velocity = Vector2.zero;
        if (isGrounded)
            rb.isKinematic = true;
    }

    public void SetIfMoving(bool move) {
        rb.velocity = Vector3.zero;
        moveInput = 0;
        //anim.SetFloat("speed", 0);
        isMoving = move;
    }
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        if(groundcheck)
            Gizmos.DrawWireCube(groundcheck.position, groundCheckSize);
    }
}
