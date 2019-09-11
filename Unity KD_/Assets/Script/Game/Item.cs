using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Game;

public class Item : MonoBehaviour
{
    [SerializeField]
    GameObject wall;
    GameManeger mane;
    MainStateClass stateClass;
    PlayerMove player;

    public enum ItemST
    {
        Recover,
        Key
    }
    string[] ItemName =
    {
        "回復アイテム","脱出キー",
    };
    //Instanceするときここを変える
    public ItemST ST;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        mane = GameObject.Find("GameManeger").GetComponent<GameManeger>();
        wall = GameObject.Find("Door");
        stateClass = (MainStateClass)mane.NowManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (ST)
            {
                //回復アイテムの効果
                case ItemST.Recover:
                    player.GetComponent<PlayerMove>().GetSetHP += 10;
                    Destroy(gameObject);
                    break;
                //鍵の効果
                case ItemST.Key:
                    Destroy(wall);
                    Destroy(gameObject);
                    MainStateClass._My.GetKey = true;
                    break;
            }
        }
    }
}
