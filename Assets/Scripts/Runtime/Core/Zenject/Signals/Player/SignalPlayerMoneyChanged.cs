using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Player
{
    public class SignalPlayerMoneyChanged
    {
        public int UserMoneyAmount;

        public SignalPlayerMoneyChanged(int newUserMoneyAmount)
        {
            UserMoneyAmount = newUserMoneyAmount;
        }
    }
}