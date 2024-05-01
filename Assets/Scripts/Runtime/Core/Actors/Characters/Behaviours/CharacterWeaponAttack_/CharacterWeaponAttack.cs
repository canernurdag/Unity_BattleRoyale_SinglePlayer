using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_
{
    public class CharacterWeaponAttack : MonoBehaviour, ICharacterWeaponAttack
    {

        #region DI REF
        private ManagerInput _managerInput;
        private ManagerGame _managerGame;
        private CharacterGameplay _characterGameplay;
        private WeaponAimIndicator[] _weaponAimIndicators;

        #endregion
        public WeaponAttack ActiveWeaponAttack { get; private set; }
        public WeaponAimIndicator ActiveWeaponAimIndicator { get; private set; }

        #region THROWABLE
        [SerializeField] private float _throwableTargetSensitivity = 1f;
        #endregion

        [Inject]
        public void Construct(ManagerInput managerInput,
            ManagerGame managerGame, CharacterGameplay characterGameplay, WeaponAimIndicator[] weaponAimIndicators)
        {
            _managerInput = managerInput;
            _managerGame = managerGame;
            _characterGameplay = characterGameplay;
            _weaponAimIndicators = weaponAimIndicators;

        }

        private void Start()
        {
            _managerInput.ObservableInputForPlayerAttack
                .Where(_ => _managerGame.CurrentGameStateType == GameStateType.GameStarted)
                .Where(x => x != Vector2.zero)
                .Where(_ => _characterGameplay.IsAlive)
                .Where(_ => ActiveWeaponAttack != null)
                .Where(_ => ActiveWeaponAttack.WeaponAttackType == WeaponAttack.Type.Throwable)
                .Subscribe(input =>
                {
                    SetThrowableTargetPosition(input);
                });
        }

        public void InitChararcterWeaponAttack()
        {
            SetActiveWeaponAimIndicator(ActiveWeaponAttack.WeaponType);

            //IN ORDER TO AVOID PARENT ROTATION EFFECT
            if (ActiveWeaponAttack.WeaponAttackType == WeaponAttack.Type.Throwable)
            {
                ActiveWeaponAimIndicator.transform.SetParent(null);
            }


        }



        private void SetThrowableTargetPosition(Vector2 input)
        {
            var aimIndicatorTransform = ActiveWeaponAimIndicator.transform;
            var inputVector = new Vector3(input.x, 0, input.y);
            inputVector *= _throwableTargetSensitivity;

            aimIndicatorTransform.position += inputVector;

            var distance = Vector3.Distance(transform.position, aimIndicatorTransform.position);
            if (distance > ActiveWeaponAttack.Range)
            {
                var direction = (aimIndicatorTransform.position - transform.position).normalized;
                aimIndicatorTransform.position = transform.position + direction * ActiveWeaponAttack.Range;
            }
        }

        public WeaponAttack GetActiveWeaponAttack()
        {
            return ActiveWeaponAttack;
        }



        public void SetActiveWeaponAttack(WeaponAttack weaponAttack)
        {
            ActiveWeaponAttack = weaponAttack;
        }

        public void SetActivenessOfWeaponAimIndicator(Weapon.Type weaponType)
        {
            for (int i = 0; i < _weaponAimIndicators.Length; i++)
            {
                var weaponAimIndicator = _weaponAimIndicators[i];
                weaponAimIndicator.gameObject.SetActive(weaponAimIndicator.WeaponType == weaponType);

            }
        }

        public WeaponAimIndicator GetActiveWeaponAimIndicator()
        {
            return ActiveWeaponAimIndicator;
        }

        public void SetActiveWeaponAimIndicator(Weapon.Type weaponType)
        {
            ActiveWeaponAimIndicator = _weaponAimIndicators.First(x => x.WeaponType == weaponType);
        }
    }
}