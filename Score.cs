using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int CurrentScore;
    public static int HighScore = 0;

    public GameObject CurrentScoreText;
    public GameObject HighScoreText;

    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.GetComponent<Text>().text = "HighScore:" + HighScore;
    }

    void Update()
    {
        CurrentScore = PlayerMovement.MaxSize - 1;
        CurrentScoreText.GetComponent<Text>().text = "Score:" + CurrentScore;

        if(Snake.GameOver == true)
        {
            if(CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt("HighScore", HighScore);
            }
        }
    }
}
