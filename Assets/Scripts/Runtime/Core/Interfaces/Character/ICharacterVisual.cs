using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface ICharacterVisual
    {
        List<CharacterVisualPart> GetCharacterVisualParts(CharacterVisual.Type characterVisualType);
        void SetActiveCharacterVisualParts(CharacterVisual.Type characterVisualType);
        void SetActivenessOfAllCharacterVisualParts(bool isActive);


    }
}