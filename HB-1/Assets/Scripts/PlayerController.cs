using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Animator anim;
    [SerializeField] private float jumpForce=350;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;
    private bool isDeath=false;
    private int coin = 0;
    private float horizontal;

    private string currentAnimName;

    private Vector3 savePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        SavePoint();
        OnInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            return;
        }
        
        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
   
        // In attack, player can't move
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // when in jumping, player can't change animation
        if (isGrounded && !isJumping)
        {

            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            //run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            //attack
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
            }
            //throw
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            } 
        }

        // Check falling
        if (rb.velocity.y < 0 && !isGrounded)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        // Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal*Time.fixedDeltaTime*speed, rb.velocity.y);

            //transform.localScale = new Vector3(horizontal, 1, 1);
            transform.rotation= Quaternion.Euler(new Vector3(0,horizontal>0?0:180,0));
        }
        //idle
        else if(isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

    }

    private void OnInit()
    {
        isDeath = false;
        isAttack = false;

        transform.position = savePoint;
       
        ChangeAnim("idle");
    }
    private bool CheckGrounded()
    {

        // Check Player on ground 
        Debug.DrawLine(transform.position + Vector3.down , transform.position + Vector3.down * 1.1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down , Vector2.down, 0.1f, groundLayer);
        if (hit.collider != null)
        {
            if(isJumping)
                return false;
            return true;
        }
        return false;

    }

    private void Attack() {
        isAttack = true;
        ChangeAnim("attack");

        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Throw() {
        isAttack = true;
        ChangeAnim("throw");

        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Jump() {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("");
    }
    private void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin") {
            coin++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone") {
            ChangeAnim("die");
            isDeath = true;

            Invoke(nameof(OnInit), 1);
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
}
