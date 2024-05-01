using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Collectables
{
    public class CollectableHealUp : Collectable
    {
        [SerializeField] private int _healthIncreaseAmount;
        protected override void Effect(CharacterGameplay characterGameplay)
        {
            var newHealth = characterGameplay.Damagable.GetHealth() + _healthIncreaseAmount;
            characterGameplay.Damagable.SetHealth(newHealth, null);
            characterGameplay.Damagable.SetHealthChange(_healthIncreaseAmount);
        }
    }
}