using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    [SerializeField] protected CombatText combatTextRecoverPrefab;
    [SerializeField] private float damage = 30;
    private float hp;
    private float timeRecover = 5.0f;
    [SerializeField] private float hpRecover = 0;
    private float timeRecoverCount;
    public bool IsDead => hp <= 0;
    public float Damage { get => damage; set => damage = value; }
    public float HpRecover { get => hpRecover; set => hpRecover = value; }
    public float TimeRecover { get => timeRecover; set => timeRecover = value; }
    public float TimeRecoverCount { get => timeRecoverCount; set => timeRecoverCount = value; }

    private string currentAnimName;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100,transform);
    }

    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnHit(float damage)
    {
        timeRecoverCount=timeRecover;
        hp -= damage;
        healthBar.SetNewHP(hp>0?hp:0);
        Instantiate(combatTextPrefab,transform.position+Vector3.up,Quaternion.identity).OnInit("-"+damage.ToString());
        if (!IsDead)
        {
            
        }
        else
        {
            OnDeath();
        }
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }
    public void Recover(float hp)
    {
        if (this.healthBar.IsFull())
        {
            return;
        }
        this.hp += hp;
        this.healthBar.SetNewHP(this.hp);
        Instantiate(combatTextRecoverPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit("+"+hp.ToString());
    }

    
}
