using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] string nextLevel;
    [SerializeField] GameObject boss;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        EditorSceneManager.LoadScene("Assets/_Game/Scenes/" + nextLevel + ".unity");
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && boss==null)
        {
            EditorSceneManager.LoadScene("Assets/_Game/Scenes/" + nextLevel + ".unity");
        }
    }
}
