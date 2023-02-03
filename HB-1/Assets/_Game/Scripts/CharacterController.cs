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
    [SerializeField] private float armor = 0;
    [SerializeField] private int undeadCount = 0;
    [SerializeField] private float hpRecover = 0;
    private float hp;
    private float timeRecover = 5.0f;
    
    private float timeRecoverCount;
    public bool IsDead => hp <= 0;
    
    private bool isUndead=false;
    public float Damage { get => damage; set => damage = value; }
    public float HpRecover { get => hpRecover; set => hpRecover = value; }
    public float TimeRecover { get => timeRecover; set => timeRecover = value; }
    public float TimeRecoverCount { get => timeRecoverCount; set => timeRecoverCount = value; }
    public float Armor { get => armor; set => armor = value; }
    public bool IsUndead { get => isUndead; set => isUndead = value; }

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
    public void ActiveUndead()
    {
        if (undeadCount <= 0)
        {
            return;
        }
        undeadCount -= 1;
        isUndead = true;
        Invoke(nameof(DeactiveUndead), 3f);
    }
    public void DeactiveUndead()
    {
        isUndead = false;
    }
    public void OnHit(float damage)
    {
        //timeRecoverCount=timeRecover;
        hp -= damage;
        if (hp < 0)
        {
            ActiveUndead();
        }
        if(hp<0 && isUndead)
        {
            hp = 1;
        }
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
