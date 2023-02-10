using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : GOSingleton<AudioManager>
{
    bool flag=false;
    public List<AudioClip> audioMenu;
    public AudioClip audioCoin;
    public AudioClip audioAttack;
    public AudioClip audioDie;
    public AudioClip audioTeleport;
    public AudioClip audioJump;
    public AudioClip audioMove;
    public AudioClip audioThrow;
    public AudioClip audioOnHit;
    [SerializeField]private AudioSource audioCurrent;
    [SerializeField] private AudioSource audioGame;
    [SerializeField] GameObject sliderAudioGame;

    private void Start()
    {
        audioCurrent = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip audioClip)
    {
        audioCurrent.clip = audioClip;
        audioCurrent.Play();
    }

    public void EnableAudioGame()
    {
        flag = flag==true?false:true;
        if (flag == true) { 
            audioGame.Play();
            sliderAudioGame.GetComponent<Slider>().value = 0.5f;
        
        } else { 
            audioGame.Stop();
            sliderAudioGame.GetComponent<Slider>().value = 0;
        }

    }

    public void SetAudioGame()
    {
        audioGame.volume = sliderAudioGame.GetComponent<Slider>().value;
        if (audioGame.volume > 0)
        {
            flag = true;
            audioGame.Play();
        }
        else
        {
            flag = false;
            audioGame.Stop();
        }
    }
}
