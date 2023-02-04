using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KunaiThrow : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10.0f;
    private PlayerController playerController;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    public void OnInit()
    {
        rb.velocity = transform.right * speed;
        Invoke(nameof(OnDeSpawn), 4f);
    }

    public void OnDeSpawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float armor = collision.GetComponent<CharacterController>().Armor;
            float damage = playerController.Damage - armor;
            damage = damage > 0 ? damage : 0;
            collision.GetComponent<EnemyController>().OnHit(damage);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDeSpawn();
        }
        if(collision.tag == "Obstacle")
        {
            OnDeSpawn();
        }
        if (collision.tag == "HideMap"&& collision.GetComponent<TilemapRenderer>().enabled==true)
        {
            OnDeSpawn();
        }
    }
}
