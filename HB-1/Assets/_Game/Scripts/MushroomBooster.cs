using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBooster : MonoBehaviour
{
    [SerializeField] float damageBooster=5f;   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damage+=damageBooster;
            Destroy(gameObject);
        }
    }
}
