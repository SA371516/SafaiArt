using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Game
{
    abstract class GameUIManager
    {
        protected GameManeger mana;
        protected GameUIManager(GameManeger Manager)
        {
            mana = Manager;
        }

        public abstract void UIChange();
    }
}
