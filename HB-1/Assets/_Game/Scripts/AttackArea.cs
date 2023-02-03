using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            //Debug.Log("Hit");
            float armor = collision.GetComponent<CharacterController>().Armor;
            float damage = characterController.Damage - armor;
            damage = damage > 0 ? damage : 0;
            collision.GetComponent<CharacterController>().OnHit(damage);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Player" || collision.collider.tag == "Enemy")
    //    {
    //        Debug.Log("Hit");
    //        collision.collider.GetComponent<CharacterController>().OnHit(30f);
    //    }
    //}
}
