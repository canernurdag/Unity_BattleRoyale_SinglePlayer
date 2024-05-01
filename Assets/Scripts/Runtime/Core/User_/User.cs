using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Runtime.Core.User_
{
    [Serializable]
    public class User
    {
        public UserData Userdata { get; private set; }
        public User(UserData userData)
        {
            Userdata = userData;
        }
    }
}