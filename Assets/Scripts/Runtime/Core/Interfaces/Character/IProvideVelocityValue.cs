using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface IProvideVelocityValue
    {
        Vector3 GetCurrentVelocity();
        float GetMaxVelocity();

    }
}