using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Associate a slider with a mixer
/// </summary>
public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMusic()
    {
        mixer.SetFloat("musicVol", Mathf.Log(slider.value) * 20);
    }
    
    public void SetSound()
    {
        mixer.SetFloat("soundVol", Mathf.Log(slider.value) * 20);
    }
}

