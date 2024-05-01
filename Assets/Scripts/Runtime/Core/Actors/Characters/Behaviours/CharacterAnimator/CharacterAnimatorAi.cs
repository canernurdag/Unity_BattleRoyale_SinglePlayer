using UniRx;
using UniRx.Triggers;
using System;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators
{
    public class CharacterAnimatorAi : CharacterAnimator
    {
        protected override void SetVelocitiesRX()
        {
            this.UpdateAsObservable()
                   .Subscribe(_ => _forwardVelocity = _provideVelocityValue.GetCurrentVelocity().magnitude);

        }
    }
}