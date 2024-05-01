using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponGizmos
{
    public class WeaponGizmoDistance : MonoBehaviour
    {
        [SerializeField] private float _distance;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * _distance);
        }

    }
}