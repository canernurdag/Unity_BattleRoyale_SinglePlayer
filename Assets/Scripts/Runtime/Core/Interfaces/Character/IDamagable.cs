using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface IDamagable
    {
        int GetInitHealth();
        int GetHealth();
        float GetCurrentHealthRatio();
        void SetHealth(int health, ICharacterGameplay attackerCharacterGameplay);
        void SetHealthChange(int healthChangeAmount);
        Transform GetTransform();
    }
}