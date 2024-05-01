using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Runtime.Core.Team_;
using Assets.Scripts.Runtime.Core.User_;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions
{
    [Serializable]
    public class GameSession
    {
        public GameType SelectedGameType { get; private set; }
        public MapType SelectedMapType { get; private set; }
        public int UserCountPerTeam { get; private set; }
        public int Duration { get; private set; }
        public int DurationForCharacterRivive { get; private set; }
        public List<Team> Teams { get; private set; } = new() { new Team(), new Team() };
        public List<User> JoinedUsers { get; private set; } = new();


        public void AddUserToJoinedUsers(User user)
        {
            if (JoinedUsers.Contains(user)) return;

            JoinedUsers.Add(user);
        }

        public void RemoveUserFromJoinedUsers(User user)
        {
            if (!JoinedUsers.Contains(user)) return;

            JoinedUsers.Remove(user);
        }

        public void RemoveUserFromTeams(User user)
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                var team = Teams[i];
                if (team.Users.Contains(user))
                {
                    team.Users.Remove(user);
                }
            }
        }

        public void PlaceUserToATeam(User user, Team team)
        {
            if (team.Users.Count >= UserCountPerTeam) return;

            team.Users.Add(user);
        }

        public void PlaceUserToATeamRandomly(User user)
        {
            bool isAnyAvialableTeamExist = Teams.Any(x => x.Users.Count < UserCountPerTeam);
            if (!isAnyAvialableTeamExist) return;

            var availableTeams = Teams.Where(x => x.Users.Count < UserCountPerTeam).ToList();
            var randomTeam = availableTeams[UnityEngine.Random.Range(0, availableTeams.Count)];

            PlaceUserToATeam(user, randomTeam);


        }

        public bool IsUserConnedtedToTheGameSession(User user)
        {
            return JoinedUsers.Contains(user);
        }

        public void SetSelectedGameType(GameType gameType)
        {
            SelectedGameType = gameType;
        }

        public void SetSelectedMapType(MapType mapType)
        {
            SelectedMapType = mapType;
        }

        public void SetUserCountPerTeam(int capacity)
        {
            UserCountPerTeam = capacity;
        }

        public void SetDuration(int duration)
        {
            Duration = duration;
        }

        public void SetDurationForCharacterRivive(int duration)
        {
            DurationForCharacterRivive = duration;
        }



        public enum GameType
        {
            BattleRoyale = 0,
        }

        public enum MapType
        {
            Dust = 0,
        }
    }
}