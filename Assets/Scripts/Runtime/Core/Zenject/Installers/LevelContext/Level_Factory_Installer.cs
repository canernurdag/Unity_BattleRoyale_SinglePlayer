using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Factory_Installer : MonoInstaller
    {
        [SerializeField] private CharacterGameplayAi _prefabCharacterGameplayAi;
        [SerializeField] private CharacterGameplayPlayer _prefabCharacterGameplayPlayer;

        public override void InstallBindings()
        {
            Container.BindFactory<CharacterGameplayAi, CharacterGameplayAi.CharacterGameplayAiFactory>()
                .FromComponentInNewPrefab(_prefabCharacterGameplayAi);

            Container.BindFactory<CharacterGameplayPlayer, CharacterGameplayPlayer.CharacterGameplayPlayerFactory>()
                .FromComponentInNewPrefab(_prefabCharacterGameplayPlayer);

        }
    }
}