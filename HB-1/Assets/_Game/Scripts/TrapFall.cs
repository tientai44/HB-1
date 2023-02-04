using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float damage = 30;
    private void Update()
    {
        if (transform.position.x < player.transform.position.x+3&& transform.position.x > player.transform.position.x-3 && transform.position.y>player.transform.position.y)
        {
            Fall();
        }
    }
    private void Fall()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float dame = damage - player.Armor;
            dame = dame > 0 ? dame : 0;
            player.OnHit(dame);
            Destroy(gameObject);
        }
        else if((collision.tag == "Obstacle"))
        {
            Destroy(gameObject);
        }
    }
    
}
