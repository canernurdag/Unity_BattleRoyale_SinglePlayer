using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Systems.SaveSystem;
using Assets.Scripts.Runtime.Systems.SaveSystem.SaveDataTypes;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Save
{
    [RequireComponent(typeof(SaveSystem))]
    public class ManagerSave : MonoBehaviour
    {
        public SaveSystem SaveSystem { get; private set; }

        private void Awake()
        {
            SaveSystem = GetComponent<SaveSystem>();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus) SaveSystem.Save();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) SaveSystem.Save();

        }

        private void OnApplicationQuit()
        {
            SaveSystem.Save();
        }


        public SaveDataType_CharacterVisual GetSaveDataForCharacterVisual(CharacterVisual.Type characterVisualType)
        {
            bool isCharacterVisualTypeExistInSaveSystem = SaveSystem.SaveState.SaveDataType_CharacterVisuals
                .Any(x => x.CharacterVisualType == characterVisualType);

            if (isCharacterVisualTypeExistInSaveSystem)
            {
                return SaveSystem.SaveState.SaveDataType_CharacterVisuals
                .First(x => x.CharacterVisualType == characterVisualType);
            }
            else if (!isCharacterVisualTypeExistInSaveSystem)
            {
                Debug.Log("Character Type Does Not Exist In Save System");
            }

            return null;
        }

        public SaveDataType_WeaponVisual GetSaveDataForWeapon(Weapon.Type weaponType)
        {
            bool isWeaponTypeExistInSaveSystem = SaveSystem.SaveState.SaveDataType_Weapons
                .Any(x => x.WeaponType == weaponType);

            if (isWeaponTypeExistInSaveSystem)
            {
                return SaveSystem.SaveState.SaveDataType_Weapons
                .First(x => x.WeaponType == weaponType);
            }
            else if (!isWeaponTypeExistInSaveSystem)
            {
                Debug.Log("WeaponType Does Not Exist In Save System");
            }

            return null;
        }

        public void SetMusicLevel(SignalUserMusicLevelChanged signalUserMusicLevelChanged)
        {
            SaveSystem.SaveState.MusicLevel = signalUserMusicLevelChanged.MusicLevel;
        }

        public void SetSfxLevel(SignalUserSfxLevelChanged signalUserSfxLevelChanged)

        {
            SaveSystem.SaveState.SFXLevel = signalUserSfxLevelChanged.SfxLevel;
        }
            
            
    }
}

