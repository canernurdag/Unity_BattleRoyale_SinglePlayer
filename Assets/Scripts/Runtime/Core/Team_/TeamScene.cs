using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Team_
{
    public class TeamScene : MonoBehaviour
    {
        #region DIRECT REF
        [field: SerializeField] public Transform ParentTransformForCharacters { get; private set; }
        #endregion

        #region DI REF
        private List<TeamSpawnPoint> SpawnPoints = new();
        #endregion

        #region VAR
        public Team Team { get; private set; }
        public List<CharacterGameplay> TeamCharacterGameplays { get; private set; } = new();
        [field: SerializeField] public float InputCorrectionValue { get; private set; }
        #endregion




        [Inject]
        public void Construct(TeamSpawnPoint[] spawnPoints)
        {
            SpawnPoints = spawnPoints.ToList();
        }


        public void SetTeam(Team team)
        {
            Team = team;
        }

        public TeamSpawnPoint GetUnusedSpawnPointRandomly()
        {
            if (SpawnPoints.Count == 0) return null;
            if (SpawnPoints.All(x => x.UsingCharacter != null)) return null;

            var availableSpawnPoints = SpawnPoints
                .Where(x => x.UsingCharacter == null)
                .ToList();

            return availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        }

        public void AddCharacterToTeamCharacters(CharacterGameplay characterGameplay)
        {
            if (TeamCharacterGameplays.Contains(characterGameplay)) return;
            TeamCharacterGameplays.Add(characterGameplay);
        }

        public void RemoveChacterFromTeamCharacters(CharacterGameplay characterGameplay)
        {
            if (!TeamCharacterGameplays.Contains(characterGameplay)) return;
            TeamCharacterGameplays.Remove(characterGameplay);
        }
    }
}