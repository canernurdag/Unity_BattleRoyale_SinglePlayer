using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface IMovableAndRotatable
    {
        void Move(Vector3 target);
        void Rotate(Vector3 target);
        Transform GetTransform();
        void SetPreventMovementAndRotation(bool isPreventMovementAndRotation);
        void SetPreventOnlyRotation(bool isPreventOnlyRotation);
    }
}