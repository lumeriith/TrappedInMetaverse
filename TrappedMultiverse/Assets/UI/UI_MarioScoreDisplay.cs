using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_MarioScoreDisplay : MarioModeObject
{
    public TextMeshProUGUI scoreText;
    public string format = "00000000";
    public TextMeshProUGUI timeText;
    private void Update()
    {
        scoreText.text = Player.instance.score.ToString(format);
        timeText.text = ((int)Time.time).ToString("000");
    }
}
