using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterAnimatorPlayer : CharacterAnimator
{
    private ManagerInput _managerInput;

    [Inject]
    public void Construct(ManagerInput managerInput)
    {
        _managerInput = managerInput;
    }

    protected override void SetVelocitiesRX()
    {
        _managerInput.ObservableInputForPlayerAttack
            .Where(input => input != Vector2.zero)
            .Subscribe(input =>
             {
                 _direction = new Vector3(input.x,0,input.y);
                 _signedAngle = Vector3.SignedAngle(_direction, Vector3.forward, Vector3.up);
                 _targetRotation = Quaternion.AngleAxis(_signedAngle, Vector3.up);
                 _targetDirection = _targetRotation * _provideVelocityValue.GetCurrentVelocity().normalized;

                 _forwardVelocity = _targetDirection.z* _provideVelocityValue.GetCurrentVelocity().magnitude;
                 _rightVelocity = _targetDirection.x* _provideVelocityValue.GetCurrentVelocity().magnitude;
             });

        _managerInput.ObservableInputForPlayerAttack
         .Where(input => input == Vector2.zero)
         .Subscribe(input =>
         {
             _forwardVelocity = _provideVelocityValue.GetCurrentVelocity().magnitude;
             _rightVelocity = 0;
         });

    }
}
