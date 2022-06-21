using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the sound of the passing car with a random playtime (to put on an object)
/// </summary>
public class RandomCarSound : MonoBehaviour
{
    private AudioSource source;
    public float minWaitBetweenPlays = 15f;
    public float maxWaitBetweenPlays = 40f;
    public float waitTimeCountdown = -1f;
 
    void Start()
    {
        source = GetComponent<AudioSource>();
        waitTimeCountdown = Random.Range(3f, 8f);
    }
 
    void Update()
    {
        if (!source.isPlaying)
        {
            if (waitTimeCountdown < 0f)
            {
                source.Play();
                waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            }
            else
            {
                waitTimeCountdown -= Time.deltaTime;
            }
        }
    }
}
