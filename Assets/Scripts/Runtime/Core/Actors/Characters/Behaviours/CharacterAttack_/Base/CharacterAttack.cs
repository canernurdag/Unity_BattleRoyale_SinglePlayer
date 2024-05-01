using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base
{
    public abstract class CharacterAttackBase : MonoBehaviour, ICharacterAttack
    {

        #region DI REF
        public ICharacter Character { get; protected set; }
        protected CharacterWeapon _characterWeapon;
        protected CharacterWeaponAttack _characterWeaponAttack;
        protected IStateMachine _characterAttackStateMachine;
        protected ControllerMatch _controllerMatch;
        #endregion

        public List<CharacterAttackAmmo> Ammos = new();
        protected List<CharacterGameplay> _counterTeamCharacterGameplays;
        protected float _ammoReloadSpeed = 0.5f;

        [Inject]
        public void ConstructBase(Character character, CharacterWeapon characterWeapon,
            CharacterAttackStateMachine characterAttackStateMachine, ControllerMatch controllerMatch,
            CharacterAttackAmmo[] ammos, CharacterWeaponAttack characterWeaponAttack)
        {

            Character = character;
            _characterWeapon = characterWeapon;
            _controllerMatch = controllerMatch;
            _characterAttackStateMachine = characterAttackStateMachine;
            Ammos = ammos.ToList();
            _characterWeaponAttack = characterWeaponAttack;
        }

        protected virtual void Start()
        {
            SetCounterCharacters();
        }

        public CharacterAttackAmmo GetNextReadyAmmo()
        {
            bool isThereAnyReadyAmmo = Ammos.Any(x => x.Slider.value == 1);
            if (isThereAnyReadyAmmo)
            {
                return Ammos.Last(x => x.Slider.value == 1);
            }

            return null;
        }

        public void ResetAmmo()
        {
            for (int i = 0; i < Ammos.Count; i++)
            {
                var ammo = Ammos[i];
                ammo.SetSliderValue(1);
            }
        }

        protected void UpdateCharacterAttackAmmos()
        {
            int readyAmmos = Ammos.Count(x => x.Slider.value == 1);

            float currentIncreasingValue = Ammos.Last(x => x.Slider.value < 1 && x.Slider.value >= 0).Slider.value;

            for (int i = 0; i < Ammos.Count; i++)
            {
                var ammo = Ammos[i];

                if (i < readyAmmos)
                {
                    ammo.SetSliderValue(1);
                }
                else if (i == readyAmmos)
                {
                    ammo.SetSliderValue(currentIncreasingValue);
                    ammo.SetSliderValue(1, _ammoReloadSpeed);

                }
                else if (i > readyAmmos)
                {
                    ammo.SetSliderValue(0);
                }
            }

        }

        public void FillTheNextAmmo()
        {
            bool isThereAnyEmptyAmmo = Ammos.Any(x => x.Slider.value == 0);
            if (!isThereAnyEmptyAmmo) return;

            var firstAmmo = Ammos.First(x => x.Slider.value == 0);
            firstAmmo.SetSliderValue(1, _ammoReloadSpeed);
        }


        public abstract void SetAttackState(SignalCharacterAttack signalCharacterAttack);



        #region SET FUNCTIONS
        protected void SetCounterCharacters()
        {
            if(_controllerMatch == null) return;
          
            var teamScene = _controllerMatch.TeamScenes.First(x => x.TeamCharacterGameplays.Contains(Character));
            var counterTeamScene = _controllerMatch.TeamScenes.First(x => x != teamScene);
            _counterTeamCharacterGameplays = counterTeamScene.TeamCharacterGameplays;
        }

        #endregion

        #region GET FUNCTIONS
        public Transform GetTransform() { return transform; }

        public IDamagable GetClosestDamagableInRange()
        {

            bool isThereAnyAliveEnemy = _counterTeamCharacterGameplays.Any(x => x.Damagable.GetHealth() > 0);
            if (isThereAnyAliveEnemy)
            {
                var closestEnemy =
                              _counterTeamCharacterGameplays
                                  .Where(x => x.Damagable.GetHealth() > 0)
                                  .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
                                  .First().Damagable;

                return closestEnemy;
            }

            return null;
        }


        #endregion

        #region TEST HELPERS
        public void TestSetAllAmmosEmpty()
        {
            Ammos.ForEach(x => x.SetSliderValue(1, 0));
        }
        public void TestSetCharacter(ICharacter character)
        {
            Character = character;
        }

        public void TestSetCharacterAttackStateMachine(IStateMachine stateMachine)
        {

        }
        public IStateMachine TestGetCharacterAttackStateMachine()
        {
            return _characterAttackStateMachine;
        }

        #endregion


    }

}
