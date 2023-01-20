using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    private float hp;
    public bool IsDead => hp <= 0;

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
        Invoke(nameof(OnDespawn), 2f);
    }
    public void OnHit(float damage)
    {
        hp -= damage;
        healthBar.SetNewHP(hp>0?hp:0);
        Instantiate(combatTextPrefab,transform.position+Vector3.up,Quaternion.identity).OnInit(damage);
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
    

    
}
