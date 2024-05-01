using Assets.Scripts.Runtime.Core.Actors.Characters.Character_;
using Assets.Scripts.Runtime.Core.Interfaces.Weapons;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Managers.Manager_Vibration;
using Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons
{
    public abstract class WeaponAttack : MonoBehaviour, IWeaponAttack
    {
        #region DIRECT REF
        [SerializeField] protected Transform _muzzle;
        #endregion

        #region DI REF
        public Weapon Weapon { get; protected set; }
        public Character Character { get; protected set; }
        protected Level_MemoryPool_Controller _level_MemoryPool_Controller;
        protected ManagerSound _managerSound;
        protected ManagerVibration _managerVibration;
        #endregion

        #region CACHE
        [field: SerializeField] public Type WeaponAttackType { get; protected set; }
        public Weapon.Type WeaponType { get; protected set; }
        [field: SerializeField] public float Range { get; protected set; }
        public bool IsWeaponAttackFinished { get; protected set; }
        #endregion

        [Inject]
        public void Construct(Weapon weapon, Character character, Level_MemoryPool_Controller level_MemoryPool_Controller,
            ManagerSound managerSound, ManagerVibration managerVibration)
        {
            Weapon = weapon;
            Character = character;
            _level_MemoryPool_Controller = level_MemoryPool_Controller;
            _managerSound = managerSound;
            _managerVibration = managerVibration;

            SetIsWeaponAttackFinished(true);
        }

        //private void Awake()
        //{
        //    Weapon = GetComponent<Weapon>();
        //    Character = GetComponentInParent<Character>();
        //    _level_MemoryPool_Controller = FindObjectOfType<Level_MemoryPool_Controller>();
        //    _managerSound = FindObjectOfType<ManagerSound>();
        //    _managerVibration = FindObjectOfType<ManagerVibration>();

        //    SetIsWeaponAttackFinished(true);
        //}

        public void SetWeaponType(Weapon.Type weaponType)
        {
            WeaponType = weaponType;
        }

        public void SetIsWeaponAttackFinished(bool isWeaponAttackFinished)
        {
            IsWeaponAttackFinished = isWeaponAttackFinished;
        }
        public abstract void Attack();

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Range * transform.forward);
        }

        public enum Type
        {
            Projectile,
            Melee,
            Throwable
        }
    }
}