using Assets.Scripts.Runtime.Core.Team_;
using Assets.Scripts.Runtime.Core.User_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character
{
    public interface ICharacterGameplay
    {
        bool GetIsAlive();
        bool GetIsOwner();
        User GetUser();
        Team GetTeam();
        TeamScene GetTeamScene();
        void SetIsAlive(bool value);
        void Revive();
    }
}