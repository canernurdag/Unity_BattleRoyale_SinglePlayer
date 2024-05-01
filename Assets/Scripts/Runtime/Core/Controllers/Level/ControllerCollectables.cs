using Assets.Scripts.Runtime.Core.Actors.Collectables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Controllers.Level
{
    public class ControllerCollectables : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Transform _parentCollectables;
        #endregion

        public List<CollectableHealUp> CollectablesHealUps = new();

        private void Awake()
        {
            CollectablesHealUps = _parentCollectables.GetComponentsInChildren<CollectableHealUp>().ToList();
        }

        public CollectableHealUp GetRandomActiveCollectableHealUp()
        {
            bool isThereAnyActiveCollectableHealUp = CollectablesHealUps.Any(x => x.IsActive);

            if (isThereAnyActiveCollectableHealUp)
            {
                var availableCollectables = CollectablesHealUps.Where(x => x.IsActive).ToList();
                return availableCollectables[Random.Range(0, availableCollectables.Count)];
            }

            return null;

        }
    }
}