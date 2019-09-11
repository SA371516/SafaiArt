using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Game
{
    class PlayerAnimation:BaseAnimation
    {
        //public static PlayerAnimation _My;
        bool Nowkeck;
        bool H;
        int ClearAnim = 0;

        public bool Clear;
        public Vector3 MyCollider;
        public bool _hit;
        public Collider collider_;
        public List<GameObject> enemy02 = new List<GameObject>();

        private void Start()
        {
            _hit = false;
            Clear = false;
            _anim = gameObject.GetComponent<Animator>();
            MyCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size;
            audio_ = GetComponent<AudioSource>();
            particle = gameObject.transform.GetChild(3).GetComponent<ParticleSystem>();
            RecustTime = 0;
            NowAnimplay = 1f;
        }

        public override void AnimPlay()
        {
            //clear時アニメーション
            if (Clear)
            {
                if (ClearAnim == 0)
                {
                    PlayAnimTrigger("Clear");
                    ClearAnim++;
                }
                return;
            }

            _info = _anim.GetCurrentAnimatorStateInfo(0);
            if (Input.GetKey(KeyCode.Space) && !_info.IsName("hit01") && !_hit)
            {
                PlayAnimTrigger("Attack");
                _hit = true;
                RecustTime++;
                StartCoroutine(AttackAnim("hit01"));
            }
            if (Input.GetKey(KeyCode.LeftShift) && !_info.IsName("hit02") && !_hit && RecustTime >= 7)
            {
                particle.Play();
                PlayAnimTrigger("Attack2");
                _hit = true;
                RecustTime = 0;
                StartCoroutine(AttackAnim("hit02"));
                H = true;
            }
            //攻撃時の移動無し
            if (_info.IsName("hit01") || _info.IsName("damage"))
            {
                NowAnimplay = 0;
            }
            else
            {
                NowAnimplay = 1f;
            }
        }
        public override void  PlayAnimTrigger(string A)
        {
            _anim.SetTrigger(A);
        }
        public override void PlayAnimBool(string A, bool T)
        {
            _anim.SetBool(A, T);
        }
        public override void PlayAnimFloat(string A, float F)
        {
            _anim.SetFloat(A, F);
        }

        //攻撃アニメーション処理
        protected override IEnumerator AttackAnim(string a)
        {
            _hit = true;
            //少しのタイムラグがあるため
            if (a == "hit01")
            {
                while (!_info.shortNameHash.Equals(Animator.StringToHash("hit01")))
                {
                    yield return new WaitForSeconds(1f / 60f);
                }
                Nowkeck = _info.IsName("hit01");
                audio_.Play();
            }
            else if (a == "hit02")
            {
                while (!_info.shortNameHash.Equals(Animator.StringToHash("hit02")))
                {
                    yield return new WaitForSeconds(1f / 60f);
                }
                Nowkeck = _info.IsName("hit02");
                audio_.Play();
            }

            //範囲攻撃
            if (!enemy02.Contains(null) && a == "hit02")
            {
                gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size += new Vector3(1f, 0, 1f);
                foreach (GameObject ga in enemy02)
                {
                    ga.GetComponent<Enemy>().GetSet_EMHP--;
                    ga.GetComponent<Enemy>().particle.Play();
                }
            }
            //単独攻撃
            else if (collider_ != null)
            {
                int g = Random.Range(0, enemy02.Count);
                collider_.GetComponent<Enemy>().GetSet_EMHP--;
                collider_.GetComponent<Enemy>().particle.Play();
                //onEnemy = false;
            }

            //攻撃アクション中は
            while (Nowkeck && a == "hit01")
            {
                Nowkeck = _info.IsName("hit01");
                _hit = true;
                yield return new WaitForSeconds(1f / 60f);
            }
            //範囲攻撃バージョン
            while (Nowkeck && a == "hit02")
            {
                Nowkeck = _info.IsName("hit02");
                _hit = true;
                yield return new WaitForSeconds(1f / 60f);
            }
            //攻撃アクション終了
            gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size = MyCollider;
            H = false;
            _hit = false;
        }
    }
}
