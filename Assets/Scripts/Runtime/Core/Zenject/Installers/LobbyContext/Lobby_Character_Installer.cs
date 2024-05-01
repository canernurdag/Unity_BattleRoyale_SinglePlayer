using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.Lobby;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Interfaces.Weapons;
using Assets.Scripts.Runtime.Core.Zenject.AddressablesAdapters;
using Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LobbyContext
{
    public class Lobby_Character_Installer : CharacterInstaller
    {
        [SerializeField] private CharacterLobby _prefabCharacterPlayerLobby;
        public override void InstallBindings()
        {

            //PREFABS

            Container.Bind<CharacterLobby>()
                .FromComponentInNewPrefab(_prefabCharacterPlayerLobby)
                .AsTransient();


            // CHARACTER COMPONENTS

            Container.Bind<Animator>()
                .FromComponentInChildren()
                .AsTransient()
                .WhenInjectedInto<CharacterAnimatorLobby>();

            Container.Bind<CharacterVisual>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<CharacterWeapon>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<CharacterAnimatorLobby>()
                .FromComponentInHierarchy()
                .AsCached();


            Container.Bind<SkinnedMeshRenderer>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterVisualPart>();


            Container.Bind<SkinnedMeshRenderer>()
                .FromComponentsInChildren()
                .AsTransient()
                .WhenInjectedInto<Weapon>();

            Container.Bind<MeshRenderer>()
                .FromComponentsInChildren()
                .AsTransient()
                .WhenInjectedInto<Weapon>();





            Container.Bind<CharacterVisualPart>()
              .FromComponentsInChildren()
              .AsTransient()
              .WhenInjectedInto<CharacterVisual>();


            Container.Bind<CharacterVisual>()
              .FromComponentSibling()
              .AsTransient()
              .WhenInjectedInto<CharacterLobby>();


            Container.Bind<ICharacterVisual>()
               .To<CharacterVisual>()
               .FromComponentSibling()
               .AsTransient()
               .WhenInjectedInto<CharacterLobby>();

            Container.Bind<ICharacterWeapon>()
                .To<CharacterWeapon>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterLobby>();

            Container.Bind<CharacterVisualPart>()
                .FromComponentsInChildren()
                .AsTransient()
                .WhenInjectedInto<CharacterVisual>();

            Container.Bind<AddressableAdapterWeapon>()
                 .FromComponentInChildren()
                 .AsTransient()
                 .WhenInjectedInto<CharacterWeapon>();

            Container.Bind<IWeaponAttack>()
                .To<WeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Weapon>();

            Container.Bind<Weapon>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<WeaponAttack>();

            Container.Bind<WeaponTarget>()
                .FromComponentsSibling()
                .AsTransient()
                .WhenInjectedInto<WeaponAimIndicator>();


            Container.Bind<Level_MemoryPool_Controller>()
                 .FromComponentInHierarchy()
                 .AsCached()
                 .WhenInjectedInto<WeaponAttack>();

            Container.Bind<CharacterWeapon>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<AddressableAdapterWeapon>();

            Container.Bind<WeaponAimIndicator>()
                .FromComponentsInChildren(true)
                .AsTransient()
                .WhenInjectedInto<CharacterWeaponAttack>();

            Container.Bind<Character>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<WeaponAttack>();


            Container.Bind<WeaponAttack>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<WeaponInteractionMelee>();

        }


    }
}