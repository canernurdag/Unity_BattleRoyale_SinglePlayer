using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.GrassActor
{
    public class Grass : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        public Material NormalMaterial;
        public Material TransparentMaterial;

        [Inject]
        public void Construct(MeshRenderer meshRenderer)
        {
            _meshRenderer = meshRenderer;
        }
        public void SetMaterial(Material material)
        {
            if (!_meshRenderer) return;

            _meshRenderer.material = material;
        }
    }
}