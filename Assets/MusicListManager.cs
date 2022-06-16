using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicListManager : MonoBehaviour
{
    public AudioSource source;

    public List<AudioClip> musics;

    private int musicIndex = 0;

    public GameObject PauseMenu;
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
        if (!source.isPlaying && !PauseMenu.activeSelf)
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
    }
}
