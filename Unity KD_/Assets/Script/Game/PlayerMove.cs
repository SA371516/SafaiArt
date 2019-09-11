using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Game;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    int HP;
    public int GetSetHP
    {
        get { return HP; }
        set { HP = value; }
    }
    BaseAnimation AnimationClass;
    GameUIManager UIMG;
    
    float speed = 5f;
    public bool Clear;
    public PlayerMove()
    {
        HP = 100;
    }
    void Start()
    {
        AnimationClass = gameObject.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationClass.AnimPlay();

        #region====移動処理=====
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Quaternion Q = gameObject.transform.rotation;
        //hが入力されたら回転
        if (h != 0)
        {
            if (v == 0) AnimationClass.PlayAnimBool("Move2", true);
            else AnimationClass.PlayAnimBool("Move2", false);
            transform.Rotate(new Vector3(0, h * 100f  * Time.deltaTime, 0));
        }
        else AnimationClass.PlayAnimBool("Move2", false);
        //移動アニメーション
        if (v != 0)
        {
            AnimationClass.PlayAnimBool("Move", true);
            AnimationClass.PlayAnimFloat("run", v * AnimationClass.GetSetAnimPlay);
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, v * speed * AnimationClass.GetSetAnimPlay);
            //後ろに下がるときは速度を半分にする
            if (Input.GetKey(KeyCode.S)) velocity /= 2;
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        else
        {
            AnimationClass.PlayAnimBool("Move", false);
        }
        #endregion
        //GameOver処理
        if (HP <= 0)
        {
            GameObject.Find("GameManeger").GetComponent<SceneJump>().Jump("GameOver");
        }
    }

}
