using Assets.Scripts.Runtime.Core.User_;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiSlot_CharacterForMatchMaking : MonoBehaviour
    {

        public class UiSlotCharacterForMatchMakingFactory : PlaceholderFactory<UiSlot_CharacterForMatchMaking>
        {

        }

        public User User { get; private set; }
        [SerializeField] private TMP_Text _userName;
        [SerializeField] private Image _userIconImage;

        private CharacterVisualSettings _characterVisualSettings;

        [Inject]
        public void Construct(CharacterVisualSettings characterVisualSettings)
        {
            _characterVisualSettings = characterVisualSettings;
        }

        public void SetUser(User user)
        {
            User = user;
            SetUserName(user.Userdata.UserName);

            bool isDataExist = _characterVisualSettings.CharacterVisualDatas.Any(x => x.CharacterVisualType == user.Userdata.CharacterVisualType);

            if (isDataExist)
            {
                var targetIcon = _characterVisualSettings.CharacterVisualDatas.First(x => x.CharacterVisualType == user.Userdata.CharacterVisualType).CharacterIcon;
                SetUserIcon(targetIcon);
            }
            else if (!isDataExist)
            {
                Debug.Log("Data does not exist in SO");
            }

        }
        private void SetUserName(string userName)
        {
            _userName.text = userName;
        }

        private void SetUserIcon(Sprite sprite)
        {
            _userIconImage.sprite = sprite;
        }
    }
}