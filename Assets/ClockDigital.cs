using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockDigital : MonoBehaviour
{
    public GameObject EndGamePanel;
    public int EndGameHour = 24;
    public TMP_Text textClock;
    private int hour;
    private float minute;
    public bool IsGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        EndGamePanel.SetActive(false);
        hour = 19;
        minute = 0;
    }

    // Update is called once per frame
    void Update()
    {
        minute += Time.deltaTime;
        //Debug.Log("minute = " + (int)minute);
        if ((int)minute == 60)
        {
            minute = 0;
            hour++;
        }
        
        
        textClock.text = LeadingZero(hour) + ":" + LeadingZero((int)minute);
        EndGame();
    }
    
    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }

    private void EndGame()
    {
        if (hour == EndGameHour)
        {
            EndGamePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            IsGameOver = true;
        }
    }
}
