using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_;
using Assets.Scripts.Runtime.Core.Controllers;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LobbyContext
{
    public class Lobby_Signal_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {

            //SIGNAL BINDINGS
            Container.BindSignal<SignalPlayerActiveVisualChanged>()
                .ToMethod<CharacterVisual>(x => x.SetActiveCharacterVisualParts)
                .FromResolve();

            Container.BindSignal<SignalPlayerActiveWeaponChanged>()
                .ToMethod<CharacterWeapon>(x => x.SetActiveWeapon)
                .FromResolve();

            Container.BindSignal<SignalPlayerCustomizationWeaponChanged>()
                .ToMethod<CharacterWeapon>(x => x.SetWeaponMaterialAccordingToWeaponActiveness)
                .FromResolve();

            Container.BindSignal<SignalPlayerCustomizationCharacterVisualChanged>()
                .ToMethod<CharacterVisual>(x => x.SetVisualPartsMaterialAccordingToActiveness)
                .FromResolve();

            Container.BindSignal<SignalPlayerCustomizationWeaponChanged>()
                .ToMethod<CharacterAnimatorLobby>(x => x.SetIdleAnimationAccordingToWeaponType)
                .FromResolve();

            Container.Bind<ControllerMusic>()
               .FromComponentInHierarchy()
               .AsCached();

            Container.BindSignal<SignalUserMusicLevelChanged>()
                .ToMethod<ControllerMusic>(x => x.AdjustAudioSourceVolume)
                .FromResolve();
        }
    }
}