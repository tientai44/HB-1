using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {            
            Debug.Log("Hit");
            collision.GetComponent<CharacterController>().OnHit(30f);
           
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
