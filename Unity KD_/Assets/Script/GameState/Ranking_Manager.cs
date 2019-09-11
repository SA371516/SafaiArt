using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Game;

public class Ranking_Manager :BaseState
{
    GameManeger _game;

    string _playername;
    const string _url = "http://localhost/2018_5B/ranking_KD.php";
    InputField _inputField;
    GameObject _resultPanel;
    GameObject _panel;
    Text _resulttext;
    Text _scoreText;

    bool firstTime;
    bool chack = true;

    public Ranking_Manager(GameManeger gm,int score,bool send_score)
    {
        _game = gm;
        this.Score = score;
        chack= send_score;
    }
    public override void InstanceManager()
    {
        _playername = "";
        firstTime = false;
        _resultPanel = GameObject.Find("ResultPanel");
        _panel = GameObject.Find("Panel");
        _inputField = _panel.transform.GetChild(0).GetComponent<InputField>();
        _scoreText = _panel.transform.GetChild(3).GetComponent<Text>();
        if (_scoreText == null)
        {
            Debug.Log("null");
        }
        _resulttext = _resultPanel.transform.GetChild(0).GetComponent<Text>();

        _panel.SetActive(false);
        _resultPanel.SetActive(false);
        //ゲームクリアSceneから来た場合
        if (chack)
        {
            firstTime = true;
            _panel.SetActive(true);
            _inputField.Select();
        }
        else
        {
            _game.ClickButton("Road");
        }
    }
    public override void Manager()
    {
        _scoreText.text=  "Score   :  " +Score.ToString();
    }
    public override IEnumerator SendRanking()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", _playername);
        form.AddField("point", Score);
        form.AddField("select", "No");
        string result = "";
        using (WWW www = new WWW(_url, form))
        {
            yield return www;
            result = www.text;
        }
        _resultPanel.SetActive(true);
        _resulttext.text = result;
    }
    public override IEnumerator RoadRanking(string _sort)
    {
        WWWForm form = new WWWForm();
        string result = "";
        form.AddField("select", _sort);
        using (WWW www = new WWW(_url, form))
        {
            yield return www;
            result = www.text;
        }
        _resultPanel.SetActive(true);
        _resulttext.text = result;
    }
}
