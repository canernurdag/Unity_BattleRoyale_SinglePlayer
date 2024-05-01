using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.SaveSystem.SaveDataTypes
{
    [Serializable]
    public class SaveDataType_CharacterVisual
    {
        public CharacterVisual.Type CharacterVisualType;
        public bool IsActive;
    }
}