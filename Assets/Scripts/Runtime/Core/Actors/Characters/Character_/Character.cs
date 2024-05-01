using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Character_
{
    public abstract class Character : MonoBehaviour, ICharacter
    {

        #region DI REF
        public ICharacterWeapon CharacterWeapon { get; protected set; }
        public ICharacterVisual CharacterVisual { get; protected set; }
        protected ManagerSave _managerSave;
        #endregion


        [Inject]
        public void Construct(ICharacterWeapon characterWeapon, ICharacterVisual characterVisual, ManagerSave managerSave)
        {

            CharacterWeapon = characterWeapon;
            CharacterVisual = characterVisual;
            _managerSave = managerSave;
        }


    }

}
