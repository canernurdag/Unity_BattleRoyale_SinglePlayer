using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Input
{
    public class ManagerInput : MonoBehaviour
    {

        #region DI REF
        [Inject(Id = "PlayerMovement")]
        public IInputAdapter InputProviderForPlayerMovement { get; private set; }
        [Inject(Id = "PlayerAttack")]
        public IInputAdapter InputProviderForPlayerAttack { get; private set; }
        #endregion


        #region Cache
        public IObservable<Vector2> ObservableInputForPlayerMovement { get; private set; }
        public IObservable<Vector2> ObservableInputForPlayerAttack { get; private set; }

        public float InputCorrectionAccordingToCameraAngle { get; private set; }
        #endregion



        private void Awake()
        {
            ObservableInputForPlayerMovement = this.UpdateAsObservable()
                .Select(_ => InputProviderForPlayerMovement.GetCurrentMobileInput() * InputCorrectionAccordingToCameraAngle);

            ObservableInputForPlayerAttack = this.UpdateAsObservable()
                .Select(_ => InputProviderForPlayerAttack.GetCurrentMobileInput() * InputCorrectionAccordingToCameraAngle);

        }

        public void SetInputCorrectionAccordingToCameraAngle(float correctionValue)
        {
            InputCorrectionAccordingToCameraAngle = correctionValue;
        }

    }

}


