using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiCharacterListCanvasButtons : MonoBehaviour
    {
        #region DIRECT REFERENCES
        [SerializeField]
        private Button _exitButton,
            _characterPreviousButton, _characterNextButton,
            _weaponPreviousButton, _weaponNextButton, _userNameChangeCanvasButton;

        [SerializeField] private Button _weaponUnlockButton, _characterUnlockButton, _readyButton;
        [SerializeField] private AudioClip _buttonClickSFX, _readyButtonClickSFX, _unlockButtonSFX;
        #endregion

        #region DI REFERENCES
        private UiCanvasSystem _uiCanvasSystem;
        private SignalBus _signalBus;
        private ManagerSave _managerSave;
        private CharacterVisualSettings _characterVisualSettings;
        private WeaponSettings _weaponSettings;
        private ControllerUiCharacterListCanvas _controllerUiCharacterListCanvas;
        private ManagerSound _managerSound;
        #endregion

        private CharacterVisual.Type _currentCharacterVisualType;
        private Weapon.Type _currentWeaponType;

        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, SignalBus signalBus, ManagerSave managerSave,
         CharacterVisualSettings characterVisualSettings, WeaponSettings weaponSettings,
         ControllerUiCharacterListCanvas controllerUiCharacterListCanvas, ManagerSound managerSound)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _signalBus = signalBus;
            _managerSave = managerSave;
            _characterVisualSettings = characterVisualSettings;
            _weaponSettings = weaponSettings;
            _controllerUiCharacterListCanvas = controllerUiCharacterListCanvas;
            _managerSound = managerSound;
        }

        private void OnEnable()
        {
            if (!_managerSave) return;
            if (!_managerSave.SaveSystem) return;
            if (!_managerSave.SaveSystem.IsLoadFinished) return;


            UpdateUnlockAndSelectButtons();

        }



        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            SetButtonFunctions();
            UpdateUnlockAndSelectButtons();

        }
        private void SetButtonFunctions()
        {
            _exitButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasHome, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);


                //RESET CHARACTER TO THE PREVIOUS SELECTION
                _signalBus.Fire(new SignalPlayerActiveVisualChanged((CharacterVisual.Type)_managerSave.SaveSystem.SaveState.SelectedCharacterVisualType, true));
                _signalBus.Fire(new SignalPlayerActiveWeaponChanged((Weapon.Type)_managerSave.SaveSystem.SaveState.SelectedWeaponType, true));
            });

            _readyButton.onClick.AddListener(() =>
            {

                _managerSave.SaveSystem.SaveState.SelectedCharacterVisualType = _currentCharacterVisualType;
                _managerSave.SaveSystem.SaveState.SelectedWeaponType = _currentWeaponType;
                _managerSound.PlayAudioClipInPoint(_readyButtonClickSFX, Vector3.zero);

                _signalBus.Fire(new SignalPlayerActiveVisualChanged(_currentCharacterVisualType, true));
                _signalBus.Fire(new SignalPlayerActiveWeaponChanged(_currentWeaponType, true));

                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasHome, true);
            });

            _userNameChangeCanvasButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasChangeName, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _characterPreviousButton.onClick.AddListener(() =>
            {
                var previosCharacterVisualType = _controllerUiCharacterListCanvas.GetPreviousCharacterVisualType(_currentCharacterVisualType);
                SetCurrentCharacterVisualType(previosCharacterVisualType);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

                var saveData = _managerSave.GetSaveDataForCharacterVisual(_currentCharacterVisualType);
                if (saveData != null)
                {
                    _signalBus.Fire(new SignalPlayerCustomizationCharacterVisualChanged(_currentCharacterVisualType, saveData.IsActive));

                    UpdateUnlockAndSelectButtons();
                }

            });

            _characterNextButton.onClick.AddListener(() =>
            {
                var nextCharacterVisualType = _controllerUiCharacterListCanvas.GetNextCharacterVisualType(_currentCharacterVisualType);
                SetCurrentCharacterVisualType(nextCharacterVisualType);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

                var saveData = _managerSave.GetSaveDataForCharacterVisual(_currentCharacterVisualType);
                if (saveData != null)
                {
                    _signalBus.Fire(new SignalPlayerCustomizationCharacterVisualChanged(_currentCharacterVisualType, saveData.IsActive));

                    UpdateUnlockAndSelectButtons();
                }

            });

            _weaponPreviousButton.onClick.AddListener(() =>
            {
                var previousWeaponType = _controllerUiCharacterListCanvas.GetPreviousWeaponType(_currentWeaponType);
                SetCurrentWeaponType(previousWeaponType);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

                var saveData = _managerSave.GetSaveDataForWeapon(_currentWeaponType);
                if (saveData != null)
                {
                    _signalBus.Fire(new SignalPlayerCustomizationWeaponChanged(_currentWeaponType, saveData.IsActive));

                    UpdateUnlockAndSelectButtons();
                }

            });

            _weaponNextButton.onClick.AddListener(() =>
            {
                var nextWeaponType = _controllerUiCharacterListCanvas.GetNextWeaponType(_currentWeaponType);
                SetCurrentWeaponType(nextWeaponType);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

                var saveData = _managerSave.GetSaveDataForWeapon(_currentWeaponType);
                if (saveData != null)
                {
                    _signalBus.Fire(new SignalPlayerCustomizationWeaponChanged(_currentWeaponType, saveData.IsActive));

                    UpdateUnlockAndSelectButtons();
                }
            });


            _weaponUnlockButton.onClick.AddListener(() =>
            {
                bool isMoneyDataExist = _weaponSettings.WeaponDatas
                  .Any(x => x.WeaponType == _currentWeaponType);

                if (isMoneyDataExist)
                {
                    var requiredMoneyAmount = _weaponSettings.WeaponDatas
                    .First(x => x.WeaponType == _currentWeaponType)
                    .Price;

                    bool isEnoughMoneyExist = IsEnoughMoneyExist(requiredMoneyAmount);
                    if (!isEnoughMoneyExist) return;


                    var saveData = _managerSave.GetSaveDataForWeapon(_currentWeaponType);
                    if (saveData != null)
                    {
                        saveData.IsActive = true;
                        _managerSave.SaveSystem.SaveState.TotalMoneyAmount -= requiredMoneyAmount;
                        _signalBus.Fire(new SignalPlayerMoneyChanged((int)_managerSave.SaveSystem.SaveState.TotalMoneyAmount));
                        _signalBus.Fire(new SignalPlayerCustomizationWeaponChanged(_currentWeaponType, true));
                        UpdateUnlockAndSelectButtons();
                    }

                    _managerSound.PlayAudioClipInPoint(_unlockButtonSFX, Vector3.zero);


                }
                else if (!isMoneyDataExist)
                {
                    Debug.Log("Money Data Does Not Exist in Scriptable System");
                }


            });




            _characterUnlockButton.onClick.AddListener(() =>
            {
                bool isMoneyDataExist = _characterVisualSettings.CharacterVisualDatas
                  .Any(x => x.CharacterVisualType == _currentCharacterVisualType);

                if (isMoneyDataExist)
                {
                    var requiredMoneyAmount = _characterVisualSettings.CharacterVisualDatas
                    .First(x => x.CharacterVisualType == _currentCharacterVisualType)
                    .Price;

                    bool isEnoughMoneyExist = IsEnoughMoneyExist(requiredMoneyAmount);
                    if (!isEnoughMoneyExist) return;


                    var saveData = _managerSave.GetSaveDataForCharacterVisual(_currentCharacterVisualType);
                    if (saveData != null)
                    {
                        saveData.IsActive = true;
                        _managerSave.SaveSystem.SaveState.TotalMoneyAmount -= requiredMoneyAmount;
                        _signalBus.Fire(new SignalPlayerMoneyChanged((int)_managerSave.SaveSystem.SaveState.TotalMoneyAmount));
                        _signalBus.Fire(new SignalPlayerCustomizationCharacterVisualChanged(_currentCharacterVisualType, true));
                        UpdateUnlockAndSelectButtons();
                    }

                    _managerSound.PlayAudioClipInPoint(_unlockButtonSFX, Vector3.zero);

                }
                else if (!isMoneyDataExist)
                {
                    Debug.Log("Money Data Does Not Exist in Scriptable System");
                }
            });


        }

        public void UpdateUnlockAndSelectButtons()
        {
            UpdateUnlockButtonCharacterVisual();
            UpdateUnlockButtonWeapon();
            UpdateSelectButton();

            string characterDescription = _characterVisualSettings.CharacterVisualDatas.First(x => x.CharacterVisualType == _currentCharacterVisualType).Description;
            string weaponDescription = _weaponSettings.WeaponDatas.First(x => x.WeaponType == _currentWeaponType).Description;

            _controllerUiCharacterListCanvas.SetPlayerCharacterDescriptionInUi(characterDescription);
            _controllerUiCharacterListCanvas.SetPlayerWeaponDescriptionInUi(weaponDescription);
        }

        private void UpdateSelectButton()
        {
            bool isCurrentWeaponActive = _managerSave.SaveSystem.SaveState.SaveDataType_Weapons
            .First(x => x.WeaponType == _currentWeaponType)
            .IsActive;

            bool isCurrentCharacterVisualActive = _managerSave.SaveSystem.SaveState.SaveDataType_CharacterVisuals
                .First(x => x.CharacterVisualType == _currentCharacterVisualType)
                .IsActive;
            _readyButton.interactable = isCurrentCharacterVisualActive && isCurrentWeaponActive;
        }

        private void UpdateUnlockButtonWeapon()
        {
            bool isCurrentWeaponTypeExist = _managerSave.SaveSystem.SaveState.SaveDataType_Weapons
                .Any(x => x.WeaponType == _currentWeaponType);

            if (isCurrentWeaponTypeExist)
            {

                bool isCurrentWeaponActive = _managerSave.SaveSystem.SaveState.SaveDataType_Weapons
                .First(x => x.WeaponType == _currentWeaponType)
                .IsActive;



                _controllerUiCharacterListCanvas.SetActivenessOfWeaponLockedGameObjectInUi(!isCurrentWeaponActive);

                if (isCurrentWeaponActive)
                {
                    _weaponUnlockButton.interactable = false;
                    _controllerUiCharacterListCanvas.SetWeaponUnlockButtonTextInUi("UNLOCKED");
                }
                else if (!isCurrentWeaponActive)
                {
                    var requiredMoneyAmount = _weaponSettings.WeaponDatas
                        .First(x => x.WeaponType == _currentWeaponType)
                        .Price;

                    bool isEnoughMoneyExist = IsEnoughMoneyExist(requiredMoneyAmount);
                    _weaponUnlockButton.interactable = isEnoughMoneyExist;
                    _controllerUiCharacterListCanvas.SetWeaponUnlockButtonTextInUi(requiredMoneyAmount.ToString());
                }
            }
            else if (!isCurrentWeaponTypeExist)
            {
                Debug.Log("Current Weapon Type Does not exist in save system");
            }
        }

        private void UpdateUnlockButtonCharacterVisual()
        {
            bool isCurrentCharacterVisualTypeExist = _managerSave.SaveSystem.SaveState.SaveDataType_CharacterVisuals
                .Any(x => x.CharacterVisualType == _currentCharacterVisualType);

            if (isCurrentCharacterVisualTypeExist)
            {
                bool isCurrentCharacterVisualActive = _managerSave.SaveSystem.SaveState.SaveDataType_CharacterVisuals
                .First(x => x.CharacterVisualType == _currentCharacterVisualType)
                .IsActive;

                _controllerUiCharacterListCanvas.SetActivenessOfCharacterVisualLockedGameObjectInUi(!isCurrentCharacterVisualActive);

                if (isCurrentCharacterVisualActive)
                {
                    _characterUnlockButton.interactable = false;
                    _controllerUiCharacterListCanvas.SetCharacterUnlockButtonTextInUi("UNLOCKED");
                }

                else if (!isCurrentCharacterVisualActive)
                {
                    var requiredMoneyAmount = _characterVisualSettings.CharacterVisualDatas
                        .First(x => x.CharacterVisualType == _currentCharacterVisualType)
                        .Price;

                    bool isEnoughMoneyExist = IsEnoughMoneyExist(requiredMoneyAmount);

                    _characterUnlockButton.interactable = isEnoughMoneyExist;
                    _controllerUiCharacterListCanvas.SetCharacterUnlockButtonTextInUi(requiredMoneyAmount.ToString());

                }
            }
            else if (!isCurrentCharacterVisualTypeExist)
            {
                Debug.Log("Current Character Visual Type Does not exist in save system");
            }
        }

        private bool IsEnoughMoneyExist(int targetMoneyAmount)
        {
            return _managerSave.SaveSystem.SaveState.TotalMoneyAmount >= targetMoneyAmount;
        }


        public void SetCurrentWeaponType(Weapon.Type weaponType)
        {
            _currentWeaponType = weaponType;
        }

        public void SetCurrentCharacterVisualType(CharacterVisual.Type characterVisualType)
        {
            _currentCharacterVisualType = characterVisualType;
        }
    }
}