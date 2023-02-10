using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GOSingleton<GameManager>
{
    
    private void Start()
    {
        GetInstance();
    }
    public void Goto(int level)
    {
        SceneManager.LoadScene("Level "+level.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   

}
