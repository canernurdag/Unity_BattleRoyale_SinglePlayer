using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual
{
    public class CharacterVisualPart : MonoBehaviour
    {
        [field: SerializeField] public CharacterVisual.Type CharacterVisualType { get; private set; }
        [field: SerializeField] public CharacterVisual.BodyType CharacterVisualBodyType { get; private set; }
        public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set; }


        [Inject]
        public void Construct(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            SkinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void SetMaterial(Material material)
        {
            if (!SkinnedMeshRenderer) return;

            SkinnedMeshRenderer.material = material;
        }


    }
}