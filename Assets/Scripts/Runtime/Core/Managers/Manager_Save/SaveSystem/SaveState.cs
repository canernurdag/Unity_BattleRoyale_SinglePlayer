using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Systems.SaveSystem.SaveDataTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Runtime.Systems.SaveSystem
{
    [Serializable]
    public class SaveState
    {
        #region SAVE DATA
        public string BundleVersion;
        #endregion

        #region USER DATA
        public int UserId;
        public string UserName;
        public int TotalMoneyAmount;

        public CharacterVisual.Type SelectedCharacterVisualType;
        public Weapon.Type SelectedWeaponType;
        public List<SaveDataType_CharacterVisual> SaveDataType_CharacterVisuals;
        public List<SaveDataType_WeaponVisual> SaveDataType_Weapons;

        public int ExperienceLevel;
        public int ExperienceAmount;
        #endregion

        #region PREF DATA
        public float SFXLevel;
        public float MusicLevel;
        public bool IsVibrationOn;
        public bool IsNoAdsOn;
        #endregion


        public void SetInitialValues()
        {
            #region SAVE DATA
         
//#if UNITY_ANDROID
//            BundleVersion = PlayerSettings.Android.bundleVersionCode.ToString();
//#endif

//#if UNITY_IPHONE
//            BundleVersion = PlayerSettings.iOS.buildNumber.ToString(); 
//#endif

            #endregion

            #region USER DATA
            UserId = 1;
            UserName = "Player";
            TotalMoneyAmount = 5000;

            SelectedCharacterVisualType = CharacterVisual.Type.Male;
            SelectedWeaponType = Weapon.Type.AssaultRifle;

            SaveDataType_CharacterVisuals = new();
            var characterVisualTypeEnums = Enum.GetValues(typeof(CharacterVisual.Type)).Cast<CharacterVisual.Type>().ToList();
            for (int i = 0; i < characterVisualTypeEnums.Count; i++)
            {
                var characterVisualType = characterVisualTypeEnums[i];
                SaveDataType_CharacterVisual saveDataType_CharacterVisual = new();
                saveDataType_CharacterVisual.CharacterVisualType = characterVisualType;
                saveDataType_CharacterVisual.IsActive = characterVisualType == CharacterVisual.Type.Male;

                SaveDataType_CharacterVisuals.Add(saveDataType_CharacterVisual);
            }

            SaveDataType_Weapons = new();
            var weaponsTypeEnums = Enum.GetValues(typeof(Weapon.Type)).Cast<Weapon.Type>().ToList();
            for (int i = 0; i < weaponsTypeEnums.Count; i++)
            {
                var weaponType = weaponsTypeEnums[i];
                SaveDataType_WeaponVisual saveDataType_Weapon = new();
                saveDataType_Weapon.WeaponType = weaponType;
                saveDataType_Weapon.IsActive = weaponType == Weapon.Type.AssaultRifle;

                SaveDataType_Weapons.Add(saveDataType_Weapon);
            }


            ExperienceLevel = 1;
            ExperienceAmount = 0;

#endregion

            #region PREF DATA
                        SFXLevel = 0.5f;
                        MusicLevel = 0.5f;
                        IsVibrationOn = false;
                        IsNoAdsOn = false;
            #endregion
        }
    }

}
