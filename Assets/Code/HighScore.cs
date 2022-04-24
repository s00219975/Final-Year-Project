using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text ScoreText;

    void Update()
    {
        ScoreText.text = Mathf.FloorToInt(Time.timeSinceLevelLoad).ToString();
    }
}
