using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement
{
    public abstract class CharacterMovement : MonoBehaviour, IMovableAndRotatable
    {

        #region DI REF
        protected ManagerGame _managerGame;
        protected CharacterControllerSettings _characterControllerSettings;
        protected CharacterGameplay _characterGameplay;
        protected CharacterWeaponAttack _characterWeaponAttack;
        #endregion

        public bool PreventMovementAndRotation { get; protected set; } = false;
        public bool IsMoving { get; protected set; }
        public bool PreventOnlyRotation { get; protected set; }

        [Inject]
        public virtual void Construct(CharacterControllerSettings characterControllerSettings, CharacterGameplay characterGameplay,
            ManagerGame managerGame, CharacterWeaponAttack characterWeaponAttack)
        {
            _managerGame = managerGame;

            _characterGameplay = characterGameplay;
            _characterControllerSettings = characterControllerSettings;
            _characterWeaponAttack = characterWeaponAttack;

        }

        public virtual void Move(Vector3 target)
        {
            if (PreventMovementAndRotation) return;
        }

        public virtual void Rotate(Vector3 target)
        {
            if (PreventMovementAndRotation) return;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetIsMoving(bool isMoving)
        {
            IsMoving = isMoving;
        }

        public virtual void SetPreventMovementAndRotation(bool isPreventMovementAndRotation)
        {
            PreventMovementAndRotation = isPreventMovementAndRotation;
        }

        public void SetPreventOnlyRotation(bool isPreventOnlyRotatation)
        {
            PreventOnlyRotation = isPreventOnlyRotatation;
        }
    }
}
