using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Script.Game;


public class GameManeger : MonoBehaviour
{
    //現在のシーンに適した処理するクラスを作成
    public BaseState NowManager;

    Text ScoreText;

    static bool awake = false;
    GameManeger game;

    bool playnow = false;
    [HideInInspector]
    public string nowScene;

    private void Awake()
    {
        //Gamemanegerを別のシーンでも使うために残しておく
        if (!awake)
        {
            awake = true;
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playnow = false;
        nowScene = SceneManager.GetActiveScene().name;
        //デバッグ用
        switch (nowScene)
        {
            case "Title":
                NowManager = null;
                break;
            case "Instructions":
                NowManager = new InstructionsManager();
                NowManager.InstanceManager();
                break;
            case "MainGame":
                NowManager = new MainStateClass(this,0);
                NowManager.InstanceManager();
                playnow = true;
                break;
            case "Clear":
                NowManager = new ClearManager(0);
                NowManager.InstanceManager();
                break;
            case "GameOver":
                NowManager = new GameOverManager (0);
                NowManager.InstanceManager();
                break;
            case "Ranking":
                NowManager = new Ranking_Manager(this, 0, false);
                NowManager.InstanceManager();
                break;
        }
        //シーンが切り替わるときに処理をしてくれる
        SceneManager.activeSceneChanged += OnChange;
    }
    void Update()
    {
        if (NowManager != null)
        {
            NowManager.Manager();
        }
    }
    //シーンチェンジのときの処理
    void OnChange(Scene now, Scene next)
    {
        playnow = false;
        int Score = 0;
        if (NowManager != null)
        {
            Score = NowManager.Score;
        }
        switch (next.name)
        {
            case "Title":
                NowManager = null;
                break;
            case "Instructions":
                NowManager = new InstructionsManager();
                break;
            case "MainGame":
                NowManager = new MainStateClass(this,Score);
                playnow = true;
                break;
            case "GameOver":
                NowManager = new GameOverManager(Score);
                break;
            case "Clear":
                NowManager = new ClearManager(Score);
                Score += 3000;
                break;
            case "Ranking":
                NowManager = new Ranking_Manager(this,Score,true);
                if (nowScene == "Title")
                {
                    NowManager = new Ranking_Manager(this, Score, false);
                }
                break;
        }
        //マネージャーの初期化
        if (NowManager != null)
        {
            NowManager.InstanceManager();
        }
    }

    
    public void ClickButton(string T)
    {
        switch (T)
        {
            case "Send":
                StartCoroutine(NowManager.SendRanking());
                break;
            case "Road":
                StartCoroutine(NowManager.RoadRanking("No"));
                break;
            case "Alfa":
                StartCoroutine(NowManager.RoadRanking("alphabet"));
                break;
            case "Day":
                StartCoroutine(NowManager.RoadRanking("date"));
                break;
            case "Before":
                NowManager.PanelChange(-1);
                break;
            case "After":
                NowManager.PanelChange(1);
                break;
        }

    }

    public void Colutin(string LogText)
    {
        StartCoroutine(NowManager.TextChange(LogText));
    }

    public void GetObject(Transform trans)
    {
        GameObject _Item = Instantiate(NowManager.ItemIns(trans));
        //親子関係を解消しないと消えてしまう
        _Item.transform.parent = null;
        Colutin("回復アイテムが出現！！");
    }

}
