using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiStar : MonoBehaviour
    {
        public List<ParticleSystem> ParticleSystems;

        private void Awake()
        {
            ParticleSystems = GetComponentsInChildren<ParticleSystem>(true).ToList();
        }
    }

}
