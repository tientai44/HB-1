using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFall : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float speedValue = 200f;
    [SerializeField] private float freezeTime;
    private void Update()
    {
        if (transform.position.x < player.transform.position.x + 3 && transform.position.x > player.transform.position.x - 3 && transform.position.y > player.transform.position.y)
        {
            Fall();
        }
    }
    private void Fall()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {  
            
            if (!player.IsFreeze)
            {
                player.FreezeTimer = freezeTime;
                player.IsFreeze = true;
                player.Speed-=speedValue;
                player.SpeedDownFreeze = speedValue;
                Debug.Log("Freeze");
                
            }
            else
            {
                if (speedValue >= player.SpeedDownFreeze)
                {
                    player.FreezeTimer = freezeTime;
                    player.Speed = player.Speed + player.SpeedDownFreeze - speedValue;
                    player.SpeedDownFreeze = speedValue;

                }
            }
            Destroy(gameObject);
        }
        else if ((collision.tag == "Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
