using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons
{
    public class WeaponAimIndicator : MonoBehaviour
    {
        [field: SerializeField] public Weapon.Type WeaponType { get; private set; }
        public List<WeaponTarget> WeaponTargets { get; private set; } = new();

        [field: SerializeField] public float Range { get; private set; }

        [Inject]
        public void Construct(WeaponTarget[] weaponTargets)
        {
            WeaponTargets = weaponTargets.ToList();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.forward * Range);
        }
    }
}