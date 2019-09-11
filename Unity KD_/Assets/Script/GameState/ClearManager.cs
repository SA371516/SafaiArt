using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Game;

public class ClearManager : BaseState
{
    Text ScoreText;
    public ClearManager(int Score) : base()
    {
        this.Score = Score;
    }
    public override void InstanceManager()
    {
        Score += 3000;
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
    }
    public override void Manager()
    {
        //スコア表示処理
        if (ScoreText != null) ScoreText.text = "Score   :  " + Score.ToString();
    }
}
