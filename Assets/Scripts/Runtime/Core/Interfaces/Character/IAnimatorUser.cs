
using UnityEngine;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Systems.AnimatorSystem;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface IAnimatorUser
    {
        CharacterAnimationSettings.CharacterAnimationData GetCharacterAnimationData(Weapon.Type weaponType);
        Transform GetTransform();
        AnimatorSystem GetAnimatorSystem();
    }
}