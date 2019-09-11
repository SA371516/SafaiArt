using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Game
{
    class BaseAnimation:MonoBehaviour
    {
        protected Animator _anim;
        protected AnimatorStateInfo _info;
        protected float NowAnimplay;
        protected float RecustTime;
        [SerializeField]
        protected ParticleSystem particle;
        protected AudioSource audio_ = new AudioSource();

        public float GetSetAnimPlay
        {
            get { return NowAnimplay; }
            set { value = NowAnimplay; }
        }
        public float GetsetRecustTime
        {
            get { return RecustTime; }
            set { value = RecustTime; }
        }
        public virtual void AnimPlay()
        { 
            Debug.Log("まだ実装されていません");
        }
        public virtual void PlayAnimTrigger(string A)
        {
            Debug.Log("まだ実装されていません");
        }
        public virtual void PlayAnimBool(string A,bool T)
        {
            Debug.Log("まだ実装されていません");
        }
        public virtual void PlayAnimFloat(string A,float F)
        {
            Debug.Log("まだ実装されていません");
        }
        protected virtual IEnumerator AttackAnim(string a)
        {
            yield return new WaitForSeconds(1 / 60f);
        }
    }
}
