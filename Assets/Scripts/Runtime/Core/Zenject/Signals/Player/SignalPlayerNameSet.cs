using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Player
{
    public class SignalPlayerNameSet
    {
        public string UserName;

        public SignalPlayerNameSet(string playerName)
        {
            UserName = playerName;
        }
    }
}