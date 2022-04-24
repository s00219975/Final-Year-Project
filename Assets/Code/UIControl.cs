using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public Text ScoreText;
    public Text highScore;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        Score();
    }

    public void Score()
    {
        int score = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        ScoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1.0f;
    }
}
