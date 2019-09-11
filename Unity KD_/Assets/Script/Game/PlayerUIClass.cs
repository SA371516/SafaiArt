using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Game
{
    class PlayerUIClass:GameUIManager
    {
        Text ScoreText;
        Slider HP_ber;
        Slider Range_ber;
        GameObject _Effect;
        PlayerMove Playermove;
        PlayerAnimation AnimationClass;
        MainStateClass _stateClass;
        Text log;

        public PlayerUIClass(GameManeger manager,GameObject p, GameObject Camvas, GameObject Camvas2) : base(manager)
        {
            _stateClass = (MainStateClass)manager.NowManager;
            AnimationClass = p.GetComponent<PlayerAnimation>();
            Playermove = p.GetComponent<PlayerMove>();
            //ここからはゲームシーンのみ使う
            _Effect = Camvas.transform.GetChild(1).gameObject;
            _Effect.SetActive(false);
            //HPのスライダー処理
            HP_ber = Camvas2.transform.GetChild(1).GetComponent<Slider>();
            HP_ber.maxValue = Playermove.GetSetHP;
            //範囲攻撃可能のスライダー処理
            Range_ber = Camvas.transform.GetChild(4).GetComponent<Slider>();
            Range_ber.maxValue = 7;
            log = Camvas.transform.GetChild(3).GetComponent<Text>();
        }

        public override void UIChange()
        {
            if (_stateClass.attack.Contains(true))
            {
                _Effect.SetActive(true);
            }
            else
            {
                _Effect.SetActive(false);
            }
            //HPバーの処理
            HP_ber.value = Playermove.GetSetHP;
            //範囲攻撃バーの処理
            Range_ber.value = AnimationClass.GetsetRecustTime;

        }
    }
}
