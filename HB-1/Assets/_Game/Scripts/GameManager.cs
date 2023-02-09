using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GOSingleton<GameManager>
{
    public List<AudioClip> audioMenu;
    public AudioClip audioCoin;
    public AudioClip audioAttack;
    public AudioClip audioDie;
    public AudioClip audioTeleport;
    public AudioClip audioJump;
    public AudioClip audioMove;
    public AudioClip audioThrow;
    public AudioClip audioOnHit;
    private AudioSource audioCurrent;
    private void Start()
    {
        GetInstance();
        audioCurrent = GetComponent<AudioSource>();
    }
    public void Goto(int level)
    {
        SceneManager.LoadScene("Level "+level.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioCurrent.clip = audioClip;
        audioCurrent.Play();
    }

}
