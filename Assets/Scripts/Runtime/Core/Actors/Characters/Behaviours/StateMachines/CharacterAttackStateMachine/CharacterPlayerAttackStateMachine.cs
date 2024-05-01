using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine
{
    public class CharacterPlayerAttackStateMachine : CharacterAttackStateMachine
    {

        protected ManagerInput _managerInput;
        protected JoystickInputAdaptorPlayerAttack _joystickInputAdaptorPlayerAttack;



        [Inject]
        public void Construct(ManagerInput managerInput, JoystickInputAdaptorPlayerAttack joystickInputAdaptorPlayerAttack)
        {

            _managerInput = managerInput;
            _joystickInputAdaptorPlayerAttack = joystickInputAdaptorPlayerAttack;
        }





    }
}