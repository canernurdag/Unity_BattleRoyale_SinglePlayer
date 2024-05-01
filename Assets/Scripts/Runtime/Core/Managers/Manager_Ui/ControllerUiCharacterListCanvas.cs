using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiCharacterListCanvas : MonoBehaviour
    {
        #region DIRECT REFERENCES
        [SerializeField] private Image _userIcon;
        [SerializeField] private GameObject _characterLockedGO, _weaponLockedGO;
        [SerializeField] private TMP_Text _userName, _userCharacterDescription, _userWeaponDescription, _userMoneyAmount;
        [SerializeField] private TMP_Text _weaponUnlockButtonText, _characterUnlockButtonText;
        #endregion

        #region DI REFERENCES
        private UiCanvasSystem _uiCanvasSystem;
        private SignalBus _signalBus;
        private ManagerSave _managerSave;
        private CharacterVisualSettings _characterVisualSettings;
        private WeaponSettings _weaponSettings;
        private ControllerUiCharacterListCanvasButtons _controllerUiCharacterListCanvasButtons;
        #endregion


        private List<Weapon.Type> _weaponTypes = new();
        private List<CharacterVisual.Type> _characterVisualTypes = new();


        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, SignalBus signalBus, ManagerSave managerSave,
            CharacterVisualSettings characterVisualSettings, WeaponSettings weaponSettings, ControllerUiCharacterListCanvasButtons controllerUiCharacterListCanvasButtons)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _signalBus = signalBus;
            _managerSave = managerSave;
            _characterVisualSettings = characterVisualSettings;
            _weaponSettings = weaponSettings;
            _controllerUiCharacterListCanvasButtons = controllerUiCharacterListCanvasButtons;
        }

        private void OnEnable()
        {
            if (!_managerSave) return;
            if (!_managerSave.SaveSystem) return;
            if (!_managerSave.SaveSystem.IsLoadFinished) return;
            SetCharacterDataFromSaveSystem();
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            _weaponTypes = Enum.GetValues(typeof(Weapon.Type)).Cast<Weapon.Type>().ToList();
            _characterVisualTypes = Enum.GetValues(typeof(CharacterVisual.Type)).Cast<CharacterVisual.Type>().ToList();

            SetCharacterDataFromSaveSystem();

        }


        private void SetCharacterDataFromSaveSystem()
        {
            var userName = _managerSave.SaveSystem.SaveState.UserName;
            var userCharacterVisualType = _managerSave.SaveSystem.SaveState.SelectedCharacterVisualType;
            var userWeaponType = _managerSave.SaveSystem.SaveState.SelectedWeaponType;


            SetPlayerNameInUi(userName);
            SetPlayerIconInUi((CharacterVisual.Type)userCharacterVisualType);
            SetPlayerMoneyAmountInUi(_managerSave.SaveSystem.SaveState.TotalMoneyAmount.ToString());


            string characterDescription = _characterVisualSettings.CharacterVisualDatas.First(x => x.CharacterVisualType == userCharacterVisualType).Description;
            string weaponDescription = _weaponSettings.WeaponDatas.First(x => x.WeaponType == userWeaponType).Description;
            SetPlayerCharacterDescriptionInUi(characterDescription);
            SetPlayerWeaponDescriptionInUi(weaponDescription);

            _controllerUiCharacterListCanvasButtons.SetCurrentCharacterVisualType((CharacterVisual.Type)userCharacterVisualType);
            _controllerUiCharacterListCanvasButtons.SetCurrentWeaponType((Weapon.Type)userWeaponType);
        }


        #region SET FUNCTIONS

        //USERNAME ///////////////////////////////
        public void SetPlayerNameInUi(SignalPlayerNameSet signalUserNameSet)
        {
            string newUserName = signalUserNameSet.UserName;
            SetPlayerNameInUi(newUserName);
        }

        public void SetPlayerNameInUi(string userName)
        {
            _userName.text = userName;
        }

        //USER ICON ///////////////////////////////
        private void SetPlayerIconInUi(Sprite iconSprite)
        {
            _userIcon.sprite = iconSprite;
        }

        private void SetPlayerIconInUi(CharacterVisual.Type characterVisualType)
        {
            var targetSprite = _characterVisualSettings.CharacterVisualDatas
               .First(x => x.CharacterVisualType == characterVisualType)
               .CharacterIcon;

            SetPlayerIconInUi(targetSprite);
        }

        public void SetPlayerIconInUi(SignalPlayerActiveVisualChanged signalUserActiveVisualChanged)
        {
            var targetSprite = _characterVisualSettings.CharacterVisualDatas
                .First(x => x.CharacterVisualType == signalUserActiveVisualChanged.CharacterVisualType)
                .CharacterIcon;

            SetPlayerIconInUi(targetSprite);
        }

        public void SetPlayerIconInUi(SignalPlayerCustomizationCharacterVisualChanged signalUserCustomizationCharacterVisual)
        {
            var targetSprite = _characterVisualSettings.CharacterVisualDatas
                .First(x => x.CharacterVisualType == signalUserCustomizationCharacterVisual.CharacterVisualType)
                .CharacterIcon;

            SetPlayerIconInUi(targetSprite);
        }

        //CHARACTER DESCRIPTION ///////////////////////////////
        public void SetPlayerCharacterDescriptionInUi(string description)
        {
            _userCharacterDescription.text = description;
        }

        //WEAPON DESCRIPTION ///////////////////////////////
        public void SetPlayerWeaponDescriptionInUi(string description)
        {
            _userWeaponDescription.text = description;
        }

        //LOCK GAMEOBJECTS ///////////////////////////////
        public void SetActivenessOfWeaponLockedGameObjectInUi(bool isActive)
        {
            _weaponLockedGO.SetActive(isActive);
        }

        public void SetActivenessOfCharacterVisualLockedGameObjectInUi(bool isActive)
        {
            _characterLockedGO.SetActive(isActive);
        }


        //MONEY ///////////////////////////////
        public void SetPlayerMoneyAmountInUi(string userMoneyAmount)
        {
            _userMoneyAmount.text = userMoneyAmount;
        }

        public void SetPlayerMoneyAmountInUi(SignalPlayerMoneyChanged signalUserMoneyChanged)
        {
            var moneyAmount = signalUserMoneyChanged.UserMoneyAmount;
            SetPlayerMoneyAmountInUi(moneyAmount.ToString());
        }


        //UNLOCK BUTTON TEXTS ///////////////////////////////
        public void SetCharacterUnlockButtonTextInUi(string textValue)
        {
            _characterUnlockButtonText.text = textValue;
        }


        public void SetWeaponUnlockButtonTextInUi(string textValue)
        {
            _weaponUnlockButtonText.text = textValue;
        }


        #endregion


        #region GET HELPER FUNCTIONS
        public Weapon.Type GetNextWeaponType(Weapon.Type currentWeaponType)
        {
            int currnetIndex = _weaponTypes.IndexOf(currentWeaponType);
            int nextIndex = 0;

            if (currnetIndex == _weaponTypes.Count - 1)
            {
                nextIndex = 0;
            }
            else
            {
                nextIndex = currnetIndex + 1;
            }

            return _weaponTypes[nextIndex];
        }

        public Weapon.Type GetPreviousWeaponType(Weapon.Type currentWeaponType)
        {
            int currnetIndex = _weaponTypes.IndexOf(currentWeaponType);

            int prevIndex = 0;

            if (currnetIndex == 0)
            {
                prevIndex = _weaponTypes.Count - 1;
            }
            else
            {
                prevIndex = currnetIndex - 1;
            }

            return _weaponTypes[prevIndex];
        }

        public CharacterVisual.Type GetNextCharacterVisualType(CharacterVisual.Type currentCharacterVisualType)
        {
            int currnetIndex = _characterVisualTypes.IndexOf(currentCharacterVisualType);
            int nextIndex = 0;

            if (currnetIndex == _characterVisualTypes.Count - 1)
            {
                nextIndex = 0;
            }
            else
            {
                nextIndex = currnetIndex + 1;
            }

            return _characterVisualTypes[nextIndex];
        }

        public CharacterVisual.Type GetPreviousCharacterVisualType(CharacterVisual.Type currentCharacterVisualType)
        {
            int currnetIndex = _characterVisualTypes.IndexOf(currentCharacterVisualType);

            int prevIndex = 0;

            if (currnetIndex == 0)
            {
                prevIndex = _characterVisualTypes.Count - 1;
            }
            else
            {
                prevIndex = currnetIndex - 1;
            }

            return _characterVisualTypes[prevIndex];
        }
        #endregion



    }
}