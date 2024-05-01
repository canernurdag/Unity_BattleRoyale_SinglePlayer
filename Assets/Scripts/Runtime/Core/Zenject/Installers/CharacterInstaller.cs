using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers
{
    public class CharacterInstaller : MonoInstaller
    {
        public DiContainer GetContainer()
        {
            return Container;
        }
    }
}