using DG.Tweening;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.StateMachineSystem
{
    public abstract class BaseStateMachineSystem : MonoBehaviour, IStateMachine
    {
        public BaseState CurrentState { get; protected set; }
        public IObservable<BaseState> CurrentStateObservable;


        protected virtual void Awake()
        {

            CurrentStateObservable = this.UpdateAsObservable()
                .TakeWhile(_ => CurrentState != null)
                .Select(_ => CurrentState);
        }

        protected virtual void Start()
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                SetCurrentState(GetInitialState());
            });
        }
        public abstract BaseState GetInitialState();

        protected virtual void Update()
        {
            CurrentState?.ExecuteUpdate();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState?.ExecuteFixedUpdate();
        }

        protected virtual void LateUpdate()
        {
            CurrentState?.ExecuteLateUpdate();
        }
        public void SetCurrentState(BaseState nextState)
        {
            //if(CurrentState != null)
            //{
            //    if (CurrentState.GetType() == nextState.GetType()) return;
            //}

            CurrentState?.ExitState();
            CurrentState = nextState;
            StartCoroutine(CurrentState.EnterState());
        }

        public BaseState GetCurrentState()
        {
            return CurrentState;
        }
    }
}

