using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip bgMusic;
    public AudioClip winSound;
    public AudioClip loseSound;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = bgMusic;
        musicSource.Play();
    }

    public void PlayWin()
    {
        musicSource.Stop();
        musicSource.clip = winSound;
        musicSource.loop = false;
        musicSource.Play();
    }

    public void PlayLose()
    {
        musicSource.Stop();
        musicSource.clip = loseSound;
        musicSource.loop = false;
        musicSource.Play();
    }
}
