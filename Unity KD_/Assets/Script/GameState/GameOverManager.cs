using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Game;

public class GameOverManager :BaseState
{
    Text ScoreText;
    public GameOverManager(int score)
    {
        this.Score = score;
    }
    public override void InstanceManager()
    {
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
    }
    public override void Manager()
    {
        //スコア表示処理
        if (ScoreText != null) ScoreText.text = "Score   :  " + Score.ToString();
    }

}
