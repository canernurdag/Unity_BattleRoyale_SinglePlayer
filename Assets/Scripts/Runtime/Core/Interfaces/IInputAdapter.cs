using UniRx;
using UnityEngine;

public interface IInputAdapter
{
    Vector2 GetCurrentMobileInput();

    Transform GetTransform();
    float GetTapDuration();
    bool IsInputExist();

}
