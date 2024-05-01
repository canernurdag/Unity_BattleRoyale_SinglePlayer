using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiRank : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _playerBackgroundSprite, _otherBackgroundSprite;

        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _iconImage;

        public int Score { get; private set; }
        public int Rank { get; private set; }

        public void SetRank(int rank)
        {
            Rank = rank;
            _rank.text = rank.ToString();
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetScore(int score)
        {
            Score = score;
            _score.text = score.ToString();
        }

        public void SetIconSprite(Sprite sprite)
        {
            _iconImage.sprite = sprite;
        }



        // public Sprite GetRandomCharacterSpite()
        // {
        //     return _characterIconsSo.CharacterIcons[Random.Range(0, _characterIconsSo.CharacterIcons.Count)];
        // }
        //
        // public Sprite GetCharacterSpriteWithNo(int listNo)
        // {
        //     if (listNo >= _characterIconsSo.CharacterIcons.Count)
        //     {
        //         return null;
        //     }
        //     
        //     return _characterIconsSo.CharacterIcons[listNo];
        // }

        public void SetBackgroundImage(bool isPlayer)
        {
            if (isPlayer)
            {
                _backgroundImage.sprite = _playerBackgroundSprite;

            }
            else if (!isPlayer)
            {
                _backgroundImage.sprite = _otherBackgroundSprite;
            }
        }
    }
}

