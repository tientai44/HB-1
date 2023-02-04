using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : CharacterController
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private KunaiThrow kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce=350;
    [SerializeField] private GameObject attackArea;

    private int comboCount = 0;
    private bool isGrounded;
    private bool isRope;
    private bool isJumping;
    private bool isAttack;
    private bool isClimb;
    private bool isGlide=false;
    private bool isSleep=false;
    //private bool isDeath=false;
    private int coin = 0;
    private float horizontal;
    private float vertical;


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
        if(isGrounded&&isGlide)
        {
            Debug.Log("Stop Glide");
            StopGlide();
            return;
        }
        if(!isRope)
        {
            isClimb = false;
            if (!isGlide)
            {
                rb.gravityScale = 1;
            }
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // In attack, player can't move
        if (isAttack && isGrounded)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // when in jumping, player can't change animation
        //if (isGrounded && !isJumping)
        {

            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            //run
            if (Mathf.Abs(horizontal) > 0.1f && isGrounded &&!isClimb &&!isGlide)
            {
                ChangeAnim("run");
            }
            //attack
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (comboCount == 0)
                {
                    Attack();
                    comboCount++;
                    Invoke(nameof(ResetCombo), 2f);
                }
                else if(comboCount == 1)
                {
                    Attack1();
                    comboCount++;
                }
                else if (comboCount == 2)
                {
                    Attack2();
                    comboCount=0;
                }
            }
            //if (Input.GetKeyDown(KeyCode.K))
            //{
            //    Attack1();
            //}
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    Attack2();
            //}
            //throw
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            }
            //sleep
            if (Input.GetKeyDown(KeyCode.U))
            {
                Sleep();
            }
        }
        //Glide
        if (Input.GetKeyDown(KeyCode.I))
        {
            Glide();
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            StopGlide();
        }
        // Check Recovery
        if (isSleep)
        {
            TimeRecoverCount -= Time.deltaTime;
        }
        if (TimeRecoverCount < 0)
        {
            Recover(HpRecover);
            TimeRecoverCount = TimeRecover;
        }
        // Check falling
        if (rb.velocity.y < 0 && !isGrounded && !isRope && !isGlide)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        // Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            if (isClimb)
            {
                rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed/6, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            }
            //transform.localScale = new Vector3(horizontal, 1, 1);
            transform.rotation= Quaternion.Euler(new Vector3(0,horizontal>0?0:180,0));
        }
        //idle
        else if(isGrounded && !isClimb)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
           
        }
        //Climbing
        if (Mathf.Abs(vertical) > 0.1f)
        {
            Climb();
        }
        else if(isClimb)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed / 6, 0);
        }
        

    }
    
    override public void OnInit()
    {
        base.OnInit();
        HpRecover = 10;
        TimeRecoverCount = TimeRecover;
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
        Debug.DrawLine(transform.position  , transform.position + Vector3.down * 1.12f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position  , Vector2.down, 1.12f, groundLayer);
        if (hit.collider != null)
        {
            if(isJumping && !isGlide)
                return false;
            if (isClimb)
            {
                return false;
            }
            return true;
        }
        return false;

    }

    public void Sleep()
    {
        if (isGrounded && rb.velocity.x == 0)
        {
            isSleep = true;
            ChangeAnim("sleep");
        }
        
    }
    public void WakeUp()
    {
        if (isSleep)
        {
            TimeRecoverCount = TimeRecover;
            isSleep = false;
            ChangeAnim("idle");
            return;
        }
    }
    public void Attack() {
        WakeUp();
        if (isAttack || IsDead)
        {
            return;
        }
        isAttack = true;
        if (!isGrounded)
        {
            ChangeAnim("jumpattack");
        }
        else
        {
            ChangeAnim("attack");
        }
        ActiveAttack();
        
    }

    public void Attack1()
    {
        WakeUp();
        if (isAttack || isJumping || !isGrounded || IsDead)
        {
            return;
        }
        isAttack = true;
        ChangeAnim("attack1");
        ActiveAttack();
        
    }
    public void Attack2()
    {
        WakeUp();
        if (isAttack || isJumping || !isGrounded || IsDead)
        {
            return;
        }
        isAttack = true;
        ChangeAnim("attack2");
        ActiveAttack();
        
    }
    public void Throw() {
        WakeUp();
        if (isAttack  || IsDead)
        {
            return;
        }
        isAttack = true;
        if (!isGrounded)
        {
            ChangeAnim("jumpthrow");
        }
        else { 
            ChangeAnim("throw"); 
        }
        KunaiThrow kunai= Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation);
        kunai.PlayerController=this;
        Invoke(nameof(ResetAttack), 0.6f);
    }

    public void Jump() {
        WakeUp();
        if (isJumping || !isGrounded || IsDead)
        {
            
            return;
        }
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void ResetCombo()
    {
        comboCount = 0;
    }
    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("");
    }
    public void Climb()
    {
        if (isRope)
        {
            rb.gravityScale = 0;
            WakeUp();
            isClimb=true;
            ChangeAnim("climb");
            rb.velocity = new Vector2(rb.velocity.x, vertical * Time.deltaTime * speed);
        }
    }
    public void Glide()
    {
        if (isGrounded)
        {
            return;
        }
        isGlide = true;
        rb.gravityScale = 0.5f;
        ChangeAnim("glide");
    }
    public void StopGlide()
    {
        if (!isGlide)
        {
            return;
        }
        isGlide = false;
        rb.gravityScale = 1;
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
        Invoke(nameof(ResetAttack), 0.6f);
        Invoke(nameof(DeActiveAttack), 0.6f);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void TeleTo(float posx,float posy)
    {
        transform.position = new Vector3(posx,posy,0);
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
        if (collision.tag == "HideMap")
        {
            collision.GetComponent<TilemapRenderer>().enabled = false;
        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Rope")
        {
            isRope = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rope")
        {
            isRope = false;
        }
        if (collision.tag == "HideMap")
        {
            collision.GetComponent<TilemapRenderer>().enabled = true;
        }
    }
    
}
