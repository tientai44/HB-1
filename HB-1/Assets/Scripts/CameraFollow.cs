using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Vector3 offset= new Vector3(0, 1, -10);
    [SerializeField] private float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = player.transform.position + offset;
        transform.position=Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * speed);
    }
}
