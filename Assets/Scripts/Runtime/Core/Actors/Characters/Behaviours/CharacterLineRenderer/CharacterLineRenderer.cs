using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterLineRenderer
{
    public class CharacterLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _linePointCount = 15;
        private Transform _targetTransform = null;

        private List<Transform> _wayPoints = new();
        [SerializeField] private float _jumpPower;

        private void Start()
        {
            InitLineRenderer();
            InitWaypoints();

        }

        private void InitWaypoints()
        {
            for (int i = 0; i < _linePointCount; i++)
            {
                var wayPoint = new GameObject("Waypoint");
                wayPoint.transform.SetParent(_lineRenderer.transform);
                _wayPoints.Add(wayPoint.transform);
            }
        }

        private void InitLineRenderer()
        {
            SetActivenessOfLineRenderer(false);
            _lineRenderer.positionCount = _linePointCount;
        }

        private void Update()
        {
            if (_lineRenderer.gameObject.activeInHierarchy)
            {
                var startPos = transform.position;

                if (_targetTransform == null) return;
                var finalPos = _targetTransform.position;



                var deltaVectorXZ = (finalPos - startPos) / _linePointCount;


                for (int i = 0; i < _wayPoints.Count; i++)
                {
                    var wayPoint = _wayPoints[i];
                    float deltaVectorY = 0;
                    var wayPointPercentage = i / (float)_wayPoints.Count;

                    if (wayPointPercentage < 0.5f)
                    {
                        var wayPointPercentageIncerase = (float)wayPointPercentage / (float)0.5f;
                        deltaVectorY = DOVirtual.EasedValue(0, _jumpPower, wayPointPercentageIncerase, Ease.OutQuad);
                    }
                    else if (wayPointPercentage == 0.5f)
                    {
                        deltaVectorY = _jumpPower;
                    }
                    else if (wayPointPercentage > 0.5f)
                    {
                        var wayPointPercentageIncerase = (float)(wayPointPercentage - 0.5f) / (float)0.5f;
                        deltaVectorY = DOVirtual.EasedValue(_jumpPower, 0, wayPointPercentageIncerase, Ease.InQuad);
                    }


                    wayPoint.position = startPos + deltaVectorXZ * i + deltaVectorY * Vector3.up;

                    _lineRenderer.SetPosition(i, wayPoint.position);


                }

            }
        }

        public void SetActivenessOfLineRenderer(bool isActive)
        {
            _lineRenderer.gameObject.SetActive(isActive);
        }

        public void SetTargetTranform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

    }
}