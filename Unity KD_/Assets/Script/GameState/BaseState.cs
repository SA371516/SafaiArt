using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Game
{
    public abstract class BaseState
    {
        public int Score;
        public bool Send_Score=false;
        protected BaseState()
        {

        }
        public abstract void InstanceManager();
        public abstract void Manager();
        //MainGameのみ処理
        public  virtual GameObject ItemIns(Transform trans)
        {
            return null;
        }
        public virtual IEnumerator TextChange(string LogText)
        {
            yield return null;
        }
        public virtual IEnumerator RoadRanking(string Sort)
        {
            return null;
        }
        public virtual IEnumerator SendRanking()
        {
            return null;
        }
        public virtual  void PanelChange(int _change)
        {

        }
    }
}
