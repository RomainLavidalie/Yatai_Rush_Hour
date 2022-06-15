using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public int score;
    private TMP_Text textScore;
    
    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score : " + score;
    }
}
