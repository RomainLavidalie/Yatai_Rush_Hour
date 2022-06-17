using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicListManager : MonoBehaviour
{
    public AudioSource source;

    public List<AudioClip> musics;

    private int musicIndex = 0;

    public bool isGameOver = false;
    public AudioClip endGameMusic;

    public GameObject PauseMenu;

    private bool checkOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        source.clip = musics[musicIndex];
        source.Play();
        source.loop = false;
        musicIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying && !PauseMenu.activeSelf && !isGameOver)
        {
            musicIndex++;
            if (musicIndex > musics.Count)
            {
                //musicIndex = 0;
                source.Pause();
            }
            else
            {
                source.clip = musics[musicIndex];
                source.Play();
            }
            
        }

        if (isGameOver && checkOnce)
        {
            source.Stop();
            source.clip = null;
            source.PlayOneShot(endGameMusic);
            checkOnce = false;
        }
    }
}
