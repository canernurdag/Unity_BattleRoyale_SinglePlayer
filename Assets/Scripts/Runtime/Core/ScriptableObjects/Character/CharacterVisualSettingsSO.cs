using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterVisualSettingsSO", menuName = "CanerNurdag/Character/CharacterVisualSettingsSO")]
public class CharacterVisualSettingsSO : ScriptableObjectInstaller<CharacterVisualSettingsSO>
{
    public CharacterVisualSettings characterVisualSettings = new();
    public override void InstallBindings()
    {
        Container.BindInstance(characterVisualSettings);
    }
}

[Serializable]
public class CharacterVisualSettings
{

    public List<CharacterVisualData> CharacterVisualDatas = new();

    [Serializable]
    public class CharacterVisualData
    {
        public CharacterVisual.Type CharacterVisualType;
        public Sprite CharacterIcon;
        public int Price;
        public string Description;
    }


}