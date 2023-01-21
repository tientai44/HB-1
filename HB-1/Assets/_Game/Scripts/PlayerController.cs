using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private KunaiThrow kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce=350;
    [SerializeField] private GameObject attackArea;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;
    private bool isDeath=false;
    private int coin = 0;
    private float horizontal;
    
   

    private Vector3 savePoint;

    //private void Awake()
    //{
    //    coin = PlayerPrefs.GetInt("coin", 0);
    //}

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            return;
        }
        
        isGrounded = CheckGrounded();
        //horizontal = Input.GetAxisRaw("Horizontal");

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

    override public void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.SetCoin(coin);
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        Invoke(nameof(OnInit),1f);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        
    }
    private bool CheckGrounded()
    {

        // Check Player on ground 
        Debug.DrawLine(transform.position  , transform.position + Vector3.down * 1.1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position  , Vector2.down, 1.1f, groundLayer);
        if (hit.collider != null)
        {
            if(isJumping)
                return false;
            return true;
        }
        return false;

    }

    public void Attack() {
        if (isAttack || isJumping || !isGrounded)
        {
            return;
        }
        isAttack = true;
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(ResetAttack), 0.5f);
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw() {
        if (isAttack || isJumping || !isGrounded)
        {
            return;
        }
        isAttack = true;
        ChangeAnim("throw");
        Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation);
        Invoke(nameof(ResetAttack), 0.5f);
    }

    public void Jump() {
        if (isJumping || !isGrounded)
        {
            return;
        }
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("");
    }
    

    internal void SavePoint()
    {
        Debug.Log("Get a save point");
        savePoint = transform.position;
    }

    public bool CheckJumping()
    {
        return isJumping;
    }
    
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            //PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1);
        }
    }

    
}
