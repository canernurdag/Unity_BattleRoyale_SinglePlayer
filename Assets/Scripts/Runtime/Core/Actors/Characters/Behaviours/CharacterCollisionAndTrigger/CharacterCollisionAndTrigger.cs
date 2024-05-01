using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterCollisionAndTrigger
{
    public class CharacterCollisionAndTrigger : MonoBehaviour
    {
        private CapsuleCollider _capsuleCollider;
        private Rigidbody _rigidbody;

        [Inject]
        public void Construct(CapsuleCollider capsuleCollider)
        {
            _capsuleCollider = capsuleCollider;
            // TryGetComponent(out _capsuleCollider);
            TryGetComponent(out _rigidbody);
        }


    }
}
