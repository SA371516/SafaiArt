using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Game;

public class Attack : MonoBehaviour
{
    PlayerAnimation AnimationClass;
    private void Start()
    {
        AnimationClass = gameObject.transform.parent.GetComponent<PlayerAnimation>();
        AnimationClass.MyCollider = GetComponent<BoxCollider>().size;
    }

    private void OnTriggerStay(Collider other)
    {
        //攻撃の時に読み込む
        if (AnimationClass._hit == true)
        {
            if (other.gameObject.tag == "Enemy" && other.GetComponent<Enemy>().GetSet_EMHP > 0)
            {
                AnimationClass.collider_ = other;
                if (!AnimationClass.enemy02.Contains(other.gameObject))
                {
                    AnimationClass.enemy02.Add(other.gameObject);
                }
            }
            //else AnimationClass.onEnemy = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            AnimationClass.enemy02.Remove(other.gameObject);
        }
    }

}
