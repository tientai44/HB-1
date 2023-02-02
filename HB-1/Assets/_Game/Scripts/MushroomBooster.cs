using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBooster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damage+=5;
            Destroy(gameObject);
        }
    }
}
