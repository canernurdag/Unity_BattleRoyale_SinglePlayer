using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiKillIndicator : MonoBehaviour
    {
        [SerializeField] private Image _killerIconImage;
        [SerializeField] private Image _killedIconImage;
        [SerializeField] private TMP_Text _killerUserName;
        [SerializeField] private TMP_Text _killedUserName;

        public void SetUiKillIndicator(Sprite killerIconSprite, Sprite killedIconSprite,
            string killerUserName, string killedUserName)
        {
            _killerIconImage.sprite = killerIconSprite;
            _killedIconImage.sprite = killedIconSprite;
            _killerUserName.text = killerUserName;
            _killedUserName.text = killedUserName;
        }
    }
}