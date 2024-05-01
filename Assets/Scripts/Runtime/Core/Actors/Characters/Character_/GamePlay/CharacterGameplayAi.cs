using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay
{
    public class CharacterGameplayAi : CharacterGameplay
    {
        public class CharacterGameplayAiFactory : PlaceholderFactory<CharacterGameplayAi>
        {

        }

        protected NavMeshAgent _navMeshAgent;
        protected CharacterMovementAi _characterMovementAi;

        [Inject]
        public void Construct(NavMeshAgent navMeshAgent, CharacterMovementAi characterMovementAi)
        {
            _navMeshAgent = navMeshAgent;
            _characterMovementAi = characterMovementAi;
        }

        protected void Start()
        {
            _navMeshAgent.enabled = false;
            StartCoroutine(NavmeshActivator());
        }

        private IEnumerator NavmeshActivator()
        {
            yield return new WaitForEndOfFrame();
            _navMeshAgent.enabled = true;
        }

        public override void SetIsAlive(bool isAlive)
        {
            base.SetIsAlive(isAlive);
            _navMeshAgent.enabled = isAlive;

        }

        public override void Revive()
        {
            _characterMovementAi.GetNavMeshAgent().enabled = false;

            transform.position = _teamSpawnPoint.transform.position;
            transform.rotation = Quaternion.Euler(_teamSpawnPoint.ForwardRotationAngle);

            SetIsAlive(true);
            Damagable.SetHealth(Damagable.GetInitHealth(), null);
            Attackable.ResetAmmo();

            InitAttackState();

            DOVirtual.DelayedCall(0.1f, () =>
            {
                _characterMovementAi.GetNavMeshAgent().enabled = true;
            });
        }

    }
}