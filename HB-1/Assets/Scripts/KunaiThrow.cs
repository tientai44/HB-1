using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KunaiThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10.0f;
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
            collision.GetComponent<EnemyController>().OnHit(30f);
            OnDeSpawn();
        }
    }
}
