using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float speed = 20;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed);
    }
}
