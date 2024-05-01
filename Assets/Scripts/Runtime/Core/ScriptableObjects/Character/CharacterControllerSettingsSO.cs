using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterControllerSettingsSO", menuName = "CanerNurdag/Character/CharacterControllerSettingsSO")]
public class CharacterControllerSettingsSO : ScriptableObjectInstaller<CharacterControllerSettingsSO>
{
    public CharacterControllerSettings characterControllerSettings = new();
    public override void InstallBindings()
    {
        Container.BindInstance(characterControllerSettings);
    }

}

[Serializable]
public class CharacterControllerSettings
{

    public float SensitivityForPlayerMovement;
    public float SensitivityForPlayerRotation;
    public float MovementSpeedOfCharacterAiNavmesh;
    public float MaxMovementSpeedPlayer;
    public float MaxMovementSpeedEnemyAi;


}