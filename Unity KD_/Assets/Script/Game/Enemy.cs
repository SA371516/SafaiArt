using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Assets.Script.Game;


public class Enemy : MonoBehaviour
{
    AnimatorStateInfo info;
    int HP;
    public int GetSet_EMHP
    {
        get { return HP; }
        set { HP = value; }
    }
    GameUIManager UIMG;
    GameManeger mane;
    MainStateClass _stateClass;

    GameObject player;
    NavMeshAgent nav;
    Animator anim;
    float destime;

    bool DeadPlay = true;
    //エフェクトのため
    new　BoxCollider collider;
    Vector3 size;
    int count_ = 0;

    [HideInInspector]
    public ParticleSystem particle;

    [HideInInspector]
    public bool attack_enemy;

    AudioSource audio_;
    BaseAnimation AnimationClass;

    private void Awake()
    {
        HP = Random.Range(1, 3);
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        size = collider.size;
        player = GameObject.FindWithTag("Player");
        mane = GameObject.Find("GameManeger").GetComponent<GameManeger>();
        _stateClass = (MainStateClass)mane.NowManager;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        destime = 0f;
        audio_ = GetComponent<AudioSource>();
        particle = transform.GetChild(19).GetComponent<ParticleSystem>();
        //EnemyClassを作成する
        AnimationClass = new BaseAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);

        //アニメーションが歩きモーションの場合
        if (info.IsName("walk"))
        {
            nav.SetDestination(player.transform.position);
        }

        //消滅処理
        if (HP > 0) return;

        destime += Time.deltaTime;
        nav.SetDestination(gameObject.transform.position);
        audio_.Stop();

        BoxCollider box = gameObject.GetComponentInChildren<BoxCollider>();
        Destroy(box);
        Attack attack = GameObject.Find("BindJoints").GetComponent<Attack>();
        //エフェクト
        //範囲攻撃
        //AnimationClass.enemy02.Remove(gameObject);
        if (DeadPlay)
        {
            anim.SetTrigger("Died");
            //エフェクト
            _stateClass.attack.Clear();
            DeadPlay = false;
        }

        //消滅するとき
        if (destime > 3f)
        {
            mane.NowManager.ItemIns(gameObject.transform);
            //mane.ItemIns(gameObject.transform);
            mane.NowManager.Score += 100;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HP <= 0) return;
            //effectを開始
            _stateClass.attack.Add(true);

            //プレイヤーの方向に向く
            var ta = player.transform.position - transform.position;
            var look = Quaternion.LookRotation(ta);
            transform.localRotation = look;

            nav.SetDestination(gameObject.transform.position);

            //effectがチカチカするため
            if (count_==0)
            {
                collider.size += new Vector3(0, 0, 0.5f);
                count_++;
            }

            anim.SetBool("Attack", true);
            //ダメージのタイミング処理
            if (info.IsName("AttackEnd"))
            {
                player.GetComponent<PlayerMove>().GetSetHP -= Random.Range(1, 3);
                AnimationClass.PlayAnimTrigger("Damage");
                audio_.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Effectを消去する
            _stateClass.attack.Clear();
            //サイズをもとに戻す
            collider.size = size;
            count_ = 0;

            anim.SetBool("Attack", false);
            attack_enemy = false;
        }
    }
}
