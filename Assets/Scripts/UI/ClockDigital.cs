using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make the clock functioning and manage the End of the Game.
/// </summary>
public class ClockDigital : MonoBehaviour
{
    public GameObject EndGamePanel;
    public int StartHour = 18;
    public int StartMinutes = 30;
    public int EndGameHour = 24;
    public TMP_Text textClock;
    public TMP_Text pauseText;
    private int hour;
    private float minute;
    public bool IsGameOver = false;

    public MusicListManager musicListManager;
    
    // Start is called before the first frame update
    void Start()
    {
        EndGamePanel.SetActive(false);
        hour = StartHour;
        minute = StartMinutes;
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
    
    /// <summary>
    /// Make sure there is an 0 before the number fi there is only one.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }

    /// <summary>
    /// Stop the game and activate the endGame UI.
    /// </summary>
    private void EndGame()
    {
        if (hour == EndGameHour)
        {
            EndGamePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            IsGameOver = true;
            musicListManager.isGameOver = true;
            pauseText.text = "Fin de partie";
        }
    }
}
