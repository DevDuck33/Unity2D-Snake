using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuHighScoreText;

    void Start()
    {
        Score.HighScore = PlayerPrefs.GetInt("HighScore");
        MenuHighScoreText.GetComponent<Text>().text = "HighScore:" + Score.HighScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
