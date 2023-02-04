using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EditorSceneManager.LoadScene("Assets/_Game/Scenes/" + "Test" + ".unity");
        }
    }
}
