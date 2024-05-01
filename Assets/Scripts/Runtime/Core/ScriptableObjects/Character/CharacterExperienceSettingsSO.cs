using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterExperienceSettingsSO", menuName = "CanerNurdag/Character/CharacterExperienceSettingsSO")]
public class CharacterExperienceSettingsSO : ScriptableObjectInstaller<CharacterExperienceSettingsSO>
{
    public CharacterExperienceSettings characterExperienceSettings = new();
    public override void InstallBindings()
    {
        Container.BindInstance(characterExperienceSettings);
    }
}

[Serializable]
public class CharacterExperienceSettings
{

    public List<int> MaxExperienceAmountForTheLevel = new();

}