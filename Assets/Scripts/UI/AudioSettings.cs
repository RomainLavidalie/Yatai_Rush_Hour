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

    /// <summary>
    /// Set volume of Music mixer with slider
    /// </summary>
    public void SetMusic()
    {
        mixer.SetFloat("musicVol", Mathf.Log(slider.value) * 20);
    }
    
    /// <summary>
    /// Set volume of Sound Effect mixer with slider
    /// </summary>
    public void SetSound()
    {
        mixer.SetFloat("soundVol", Mathf.Log(slider.value) * 20);
    }
}

