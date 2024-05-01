using Assets.Scripts.Runtime.Core.User_;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Team_
{
    [Serializable]
    public class Team
    {
        #region Cache
        public List<User> Users = new();
        public int Score = 0;
        #endregion

        public void SetScore(int score)
        {
            Score = score;
        }
    }

}

