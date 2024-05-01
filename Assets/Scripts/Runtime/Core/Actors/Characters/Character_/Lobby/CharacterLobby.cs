using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using System;
using System.Collections;

using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Character_.Lobby
{
    public class CharacterLobby : Character
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            InitCharacterFromSaveSystem();
            InitCharacterToScene();
        }


        private void InitCharacterFromSaveSystem()
        {
            CharacterVisual.SetActiveCharacterVisualParts((Behaviours.CharacterVisual.CharacterVisual.Type)_managerSave.SaveSystem.SaveState.SelectedCharacterVisualType);
            CharacterWeapon.SetActiveWeapon((Weapons.Weapon.Type)_managerSave.SaveSystem.SaveState.SelectedWeaponType);
        }

        private void InitCharacterToScene()
        {
            _signalBus.Fire(new SignalPlayerCustomizationCharacterVisualChanged((Behaviours.CharacterVisual.CharacterVisual.Type)_managerSave.SaveSystem.SaveState.SelectedCharacterVisualType, true));
            _signalBus.Fire(new SignalPlayerCustomizationWeaponChanged((Weapons.Weapon.Type)_managerSave.SaveSystem.SaveState.SelectedWeaponType, true));
        }

    }
}