using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;


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
