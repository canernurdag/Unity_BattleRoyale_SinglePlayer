using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSessionSettingsSO", menuName = "CanerNurdag/GameSession/GameSessionSettingsSO")]
public class GameSessionSettingsSO : ScriptableObjectInstaller<GameSessionSettingsSO>
{
    public GameSessionSettings GameSessionSettings = new();

    public override void InstallBindings()
    {
        Container.BindInstance(GameSessionSettings);
    }
}




[Serializable]
public class GameSessionSettings
{
    public List<GameSessionData> GameSessionDatas = new();

    [Serializable]
    public class GameSessionData
    {
        public GameSession.GameType GameType;
        public string Description;
        public int Duration;
        public int UserCountPerTeam;
        public int DurationForCharacterRivive;
    }


}