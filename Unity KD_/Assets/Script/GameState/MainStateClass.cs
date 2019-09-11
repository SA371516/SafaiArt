using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Assets.Script.Game
{
    class MainStateClass:BaseState
    {
        public static MainStateClass _My;
        PlayerUIClass PlayerUI;
        GameManeger _Manager;
        GameObject _playerObject;

        GameObject[] ItemObjects;
        GameObject[] Enemy_InsPos;
        GameObject Enemy;
        Text log;
        Text ScoreText;
        float Enemy_Ins_Time;

        [HideInInspector]
        public List<bool> attack = new List<bool>();
        public bool GetKey;
        float Interval;
        public float interval;
        float Text_Interval;

        //fps処理
        int fps;
        float nextTime;
        public MainStateClass(GameManeger m,int Score) : base()
        {
            this.Score = Score;
            _My = this;
            _Manager = m;
        }
        public override void InstanceManager()
        {
            Enemy_InsPos = GameObject.FindGameObjectsWithTag("Ins");
            Enemy = (GameObject)Resources.Load("Zombie");
            ItemObjects = Resources.LoadAll<GameObject>("");
            log = GameObject.Find("Log").GetComponent<Text>();
            log.text = "";
            _playerObject = GameObject.FindWithTag("Player");
            GameObject Camvas, Camvas2;
            Camvas = GameObject.Find("Canvas1");
            Camvas2 = GameObject.Find("Canvas2");
            PlayerUI = new PlayerUIClass(_Manager, _playerObject, Camvas, Camvas2);

            attack.Clear();
            //カギ出現などのログ
            Text_Interval = 4;
            nextTime = 0f;
        }

        public override void Manager()
        {
            //fps処理
            if (nextTime > Time.time) fps++;

            //スコア表示処理
            if (ScoreText != null) ScoreText.text = "Score   :  " + Score.ToString();

            //UI管理
            PlayerUI.UIChange();

            Enemy_Ins_Time += Time.deltaTime;
            //敵出現スクリプト//fpsの判定
            if (Enemy_Ins_Time > interval && fps > 40)
            {
                //ランダムの場所に出現する
                int index = Random.Range(0, 4);
                //Instantiate(Enemy, Enemy_InsPos[index].transform);
                Enemy_Ins_Time = 0;
                fps = 0;
                nextTime = Time.time + 1;
            }
            //二度目(不安)
            if (GetKey)
            {
                _Manager.Colutin("ゴールが出現しました");
                GetKey = false;
            }
        }
        public override IEnumerator TextChange(string T)
        {
            float time = 0;
            time = Interval;
            //テキスト変更
            log.text = T;
            //透明度処理
            Color color = new Color();
            color.a = 1;
            log.color = color;
            //透明度を上げる
            while (time >= 0)
            {
                int a = -1;
                time += Time.deltaTime * a;
                color.a = time / Interval;
                log.color = color;
                yield return new WaitForSeconds(1f / 60f);

            }

        }
        //この関数を呼び、アイテム出現判定をさせる
        public override GameObject ItemIns(Transform trans)
        {
            int ItemID = Random.Range(0, 100);
            Transform pos = trans;
            //少し上にあげる処理
            pos.position += new Vector3(0f, 1f, 0f);
            GameObject _Item=null;
            //カギ出現
            if (ItemID < 5)
            {
                 _Item = Resources.Load("Item/Key") as GameObject;
                _Item.transform.position = trans.position;
            }
            //回復アイテム
            else if (ItemID > 6 && ItemID < 30)
            {
                _Item = Resources.Load("Item/Recover_Item") as GameObject;
                _Item.transform.position = trans.position;
            }
            //攻撃力アップ
            else if (ItemID > 21 && ItemID < 80)
            {

            }
            //スコアアップアイテム

            return _Item;
        }
    }
}
