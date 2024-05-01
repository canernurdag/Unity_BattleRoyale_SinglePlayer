using Assets.Scripts.Runtime.Core.Actors.GrassActor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterTrigger
{
    public class CharacterTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Grass grass = other.GetComponent<Grass>();
            if (grass)
            {
                grass.SetMaterial(grass.TransparentMaterial);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Grass grass = other.GetComponent<Grass>();
            if (grass)
            {
                grass.SetMaterial(grass.NormalMaterial);
            }

        }
    }
}