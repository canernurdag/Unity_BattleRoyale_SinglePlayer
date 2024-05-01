using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterSprite
{
    public class CharacterSprites : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private SpriteRenderer _radialSpriteRenderer;
        [field: SerializeField] public Color OwnerColor, SameTeamColor, CounterTeamColor;
        #endregion

        public void SetRadialSpriteColor(Color color)
        {
            _radialSpriteRenderer.color = color;
        }

    }
}