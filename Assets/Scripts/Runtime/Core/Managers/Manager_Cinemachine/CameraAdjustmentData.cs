using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using Assets.Scripts.Runtime.Core.Team_;

[Serializable]
public class CameraAdjustmentData
{
    public TeamScene TeamScene;
    public CinemachineVirtualCamera VirtualCamera;
    public Vector3 Position;
    public Vector3 RotationAngle;
}
