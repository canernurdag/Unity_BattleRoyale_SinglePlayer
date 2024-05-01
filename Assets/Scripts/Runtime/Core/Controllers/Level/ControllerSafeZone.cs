using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Controllers.Level
{
    public class ControllerSafeZone : MonoBehaviour
    {
        // private float _transparentValue = 0.3f;
        // private float _opaqueValue = 0.8f;
        // private Camera _mainCamera;
        //
        // [SerializeField] private LayerMask _arenaForceFieldLayerMask;
        // private Vector3 _direction;
        // private float _rayDistance;
        //
        // private Material _material;
        //
        // private void Awake()
        // {
        //     _mainCamera = Camera.main;
        //     _material = GetComponentInChildren<Renderer>().material;
        // }
        //
        // private void Start()
        // {
        //     _rayDistance = Vector3.Distance(LevelReferences.Instance.Player.transform.position,
        //         _mainCamera.transform.position);
        // }
        //
        // private void Update()
        // {
        //     _direction = (LevelReferences.Instance.Player.transform.position - _mainCamera.transform.position).normalized;
        //     Debug.DrawLine(_mainCamera.transform.position, _mainCamera.transform.position + _direction*_rayDistance, Color.blue);
        //     
        //     if(Physics.Raycast(_mainCamera.transform.position, _direction, _rayDistance, _arenaForceFieldLayerMask))
        //     {
        //         if (_material.GetFloat("_Alpha") != _transparentValue)
        //         {
        //             _material.SetFloat("_Alpha", _transparentValue);
        //         }
        //        
        //     }
        //     else
        //     {
        //         if (_material.GetFloat("_Alpha") != _opaqueValue)
        //         {
        //             _material.SetFloat("_Alpha", _opaqueValue);
        //         }
        //       
        //     }
        // }
    }
}