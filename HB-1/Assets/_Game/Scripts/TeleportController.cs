using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float timeWait=5.0f;
    private float timeCount;
    private PlayerController playerController;
    private void Start()
    {
        timeCount = timeWait;
    }
    private void Update()
    {
        if(timeCount < 0)
        {
            playerController.TeleTo(target.transform.position.x,target.transform.position.y);
            timeCount = timeWait;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            timeCount -= Time.deltaTime;
            playerController=collision.GetComponent<PlayerController>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timeCount = timeWait;
            playerController = null;
        }
    }
}
