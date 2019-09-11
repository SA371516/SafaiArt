using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJump : MonoBehaviour
{
    
    //ボタンでジャンプしなくてはいけないとき
    public void onclick()
    {
        string str=null;
        Text t = gameObject.transform.GetChild(0).GetComponent<Text>();
        //読み込みの停止を防ぐため

        switch (t.text)
        {
            case "MainGame":
                str = "Load";
                Jump(str);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "遊び方":
                str = "Instructions";
                Jump(str);
                break;
            default:
                str = t.text;
                Jump(str);
                break;
        }
    }
    //シーン移動をするための関数
    public void Jump(string Jumpname)
    {
        GameManeger game = GameObject.Find("GameManeger").GetComponent<GameManeger>();
        game.nowScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Jumpname);
    }
}
