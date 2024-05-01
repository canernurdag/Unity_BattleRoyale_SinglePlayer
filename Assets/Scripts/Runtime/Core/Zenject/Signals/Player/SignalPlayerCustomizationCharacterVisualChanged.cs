using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Player
{
    public class SignalPlayerCustomizationCharacterVisualChanged
    {
        public CharacterVisual.Type CharacterVisualType;
        public bool IsActive;

        public SignalPlayerCustomizationCharacterVisualChanged(CharacterVisual.Type characterVisualType, bool isActive)
        {
            CharacterVisualType = characterVisualType;
            IsActive = isActive;
        }
    }
}