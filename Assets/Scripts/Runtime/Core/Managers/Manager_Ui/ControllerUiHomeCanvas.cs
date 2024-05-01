using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiHomeCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Button _playButton, _charactersButton, _settingsButton;
        [SerializeField] private Image _userIcon;
        [SerializeField] private TMP_Text _userName, _userMoneyAmount;
        [SerializeField] private TMP_Text _userExperienceLevel, _userExperenceAmount;
        [SerializeField] private Slider _userExperienceSlider;
        [SerializeField] private AudioClip _buttonClickSFX, _playButtonClickSFX;
        #endregion

        #region DI REF
        private UiCanvasSystem _uiCanvasSystem;
        private ManagerSave _managerSave;
        private CharacterVisualSettings _characterVisualSettings;
        private CharacterExperienceSettings _characterExperienceSettings;
        private SignalBus _signalBus;
        private ManagerSound _managerSound;
        #endregion

        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, ManagerSave managerSave,
            CharacterVisualSettings characterVisualSettings, CharacterExperienceSettings characterExperienceSettings,
            SignalBus signalBus, ManagerSound managerSound)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _managerSave = managerSave;
            _characterVisualSettings = characterVisualSettings;
            _characterExperienceSettings = characterExperienceSettings;
            _signalBus = signalBus;
            _managerSound = managerSound;
        }


        private void OnEnable()
        {
            if (!_managerSave) return;
            if (!_managerSave.SaveSystem) return;
            if (!_managerSave.SaveSystem.IsLoadFinished) return;
            SetCharacterDataFromSaveSystem();
            _signalBus.Fire(new SignalPlayerMoneyChanged((int)_managerSave.SaveSystem.SaveState.TotalMoneyAmount));
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            SetCharacterDataFromSaveSystem();
            SetButtonFunctions();
        }



        private void SetCharacterDataFromSaveSystem()
        {
            var userName = _managerSave.SaveSystem.SaveState.UserName;
            var userCharacterVisualType = _managerSave.SaveSystem.SaveState.SelectedCharacterVisualType;


            SetPlayerNameInUi(userName);
            SetPlayerIconInUi((CharacterVisual.Type)userCharacterVisualType);
            SetPlayerMoneyAmountInUi(_managerSave.SaveSystem.SaveState.TotalMoneyAmount.ToString());
            SetPlayerExperienceLevelInUi(_managerSave.SaveSystem.SaveState.ExperienceLevel.ToString());

            int maxExpAmount = _characterExperienceSettings.MaxExperienceAmountForTheLevel[(int)_managerSave.SaveSystem.SaveState.ExperienceAmount];
            var expAmountString = $"{_managerSave.SaveSystem.SaveState.ExperienceAmount}/{maxExpAmount}";
            SetPlayerExperienceAmountInUi(expAmountString);

            float sliderValue = (float)(_managerSave.SaveSystem.SaveState.ExperienceAmount / (float)maxExpAmount);
            SetPlayerExperienceAmountSliderInUi(sliderValue);
        }


        private void SetButtonFunctions()
        {
            _playButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasLobby, true);
                _managerSound.PlayAudioClipInPoint(_playButtonClickSFX, Vector3.zero);
            });

            _charactersButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasCharacterList, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);


            });

            _settingsButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasSettings, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

        }

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


        public void SetPlayerNameInUi(SignalPlayerNameSet signalUserNameSet)
        {
            string newUserName = signalUserNameSet.UserName;
            SetPlayerNameInUi(newUserName);
        }

        public void SetPlayerNameInUi(string userName)
        {
            _userName.text = userName;
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

        public void SetPlayerExperienceLevelInUi(string userLevel)
        {
            _userExperienceLevel.text = userLevel;
        }

        public void SetPlayerExperienceAmountInUi(string userMoneyAmount)
        {
            _userExperenceAmount.text = userMoneyAmount;
        }

        public void SetPlayerExperienceAmountSliderInUi(float sliderValue)
        {
            _userExperienceSlider.value = sliderValue;
        }


    }
}