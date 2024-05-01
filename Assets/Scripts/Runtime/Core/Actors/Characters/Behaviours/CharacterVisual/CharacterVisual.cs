using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual
{
    public class CharacterVisual : MonoBehaviour, ICharacterVisual
    {
        private CharacterVisualPart[] _characterVisualParts;
        public List<CharacterVisualPart> ActiveCharacterVisualParts { get; private set; } = new();

        [field: SerializeField] public Material ActiveMaterial_Body { get; private set; }
        [field: SerializeField] public Material InActiveMaterial_Body { get; private set; }
        [field: SerializeField] public Material ActiveMaterial_Head { get; private set; }
        [field: SerializeField] public Material InActiveMaterial_Head { get; private set; }


        [Inject]
        public void Construct(CharacterVisualPart[] characterVisualParts)
        {
            _characterVisualParts = characterVisualParts;

        }



        public List<CharacterVisualPart> GetCharacterVisualParts(Type characterVisualType)
        {
            List<CharacterVisualPart> characterVisualParts = new();

            for (int i = 0; i < _characterVisualParts.Length; i++)
            {
                if (_characterVisualParts[i].CharacterVisualType == characterVisualType)
                {
                    characterVisualParts.Add(_characterVisualParts[i]);
                }
            }

            return characterVisualParts;
        }

        public void SetActiveCharacterVisualParts(Type characterVisualType)
        {
            ActiveCharacterVisualParts = new();

            for (int i = 0; i < _characterVisualParts.Length; i++)
            {
                var characterVisualPart = _characterVisualParts[i];

                if (_characterVisualParts[i].CharacterVisualType == characterVisualType)
                {
                    characterVisualPart.gameObject.SetActive(true);
                    ActiveCharacterVisualParts.Add(characterVisualPart);
                }
                else
                {
                    characterVisualPart.gameObject.SetActive(false);

                }
            }

        }

        public void SetActiveCharacterVisualParts(SignalPlayerActiveVisualChanged signalPlayerActiveVisualChanged)
        {
            ActiveCharacterVisualParts = new();

            for (int i = 0; i < _characterVisualParts.Length; i++)
            {
                var characterVisualPart = _characterVisualParts[i];

                if (_characterVisualParts[i].CharacterVisualType == signalPlayerActiveVisualChanged.CharacterVisualType)
                {
                    characterVisualPart.gameObject.SetActive(true);
                    ActiveCharacterVisualParts.Add(characterVisualPart);

                    if (signalPlayerActiveVisualChanged.IsActive)
                    {
                        if (characterVisualPart.CharacterVisualBodyType == BodyType.Body)
                        {
                            characterVisualPart.SetMaterial(ActiveMaterial_Body);
                        }
                        else if (characterVisualPart.CharacterVisualBodyType == BodyType.Head)
                        {
                            characterVisualPart.SetMaterial(ActiveMaterial_Head);
                        }
                    }
                    else if (!signalPlayerActiveVisualChanged.IsActive)
                    {
                        if (characterVisualPart.CharacterVisualBodyType == BodyType.Body)
                        {
                            characterVisualPart.SetMaterial(InActiveMaterial_Body);
                        }
                        else if (characterVisualPart.CharacterVisualBodyType == BodyType.Head)
                        {
                            characterVisualPart.SetMaterial(InActiveMaterial_Head);

                        }
                    }
                }
                else
                {
                    characterVisualPart.gameObject.SetActive(false);

                }
            }

        }

        public void SetVisualPartsMaterialAccordingToActiveness(SignalPlayerCustomizationCharacterVisualChanged signalUserCustomizationCharacterVisual)
        {
            for (int i = 0; i < _characterVisualParts.Length; i++)
            {
                var characterVisualPart = _characterVisualParts[i];

                if (_characterVisualParts[i].CharacterVisualType == signalUserCustomizationCharacterVisual.CharacterVisualType)
                {
                    characterVisualPart.gameObject.SetActive(true);

                    if (signalUserCustomizationCharacterVisual.IsActive)
                    {
                        if (characterVisualPart.CharacterVisualBodyType == BodyType.Body)
                        {
                            characterVisualPart.SetMaterial(ActiveMaterial_Body);
                        }
                        else if (characterVisualPart.CharacterVisualBodyType == BodyType.Head)
                        {
                            characterVisualPart.SetMaterial(ActiveMaterial_Head);
                        }
                    }
                    else if (!signalUserCustomizationCharacterVisual.IsActive)
                    {
                        if (characterVisualPart.CharacterVisualBodyType == BodyType.Body)
                        {
                            characterVisualPart.SetMaterial(InActiveMaterial_Body);
                        }
                        else if (characterVisualPart.CharacterVisualBodyType == BodyType.Head)
                        {
                            characterVisualPart.SetMaterial(InActiveMaterial_Head);

                        }
                    }
                }
                else
                {
                    characterVisualPart.gameObject.SetActive(false);

                }
            }
        }

        public void SetActivenessOfAllCharacterVisualParts(bool isActive)
        {
            for (int i = 0; i < _characterVisualParts.Length; i++)
            {
                var characterVisualPart = _characterVisualParts[i];
                characterVisualPart.gameObject.SetActive(isActive);
            }

        }


        ///

        public enum Type
        {
            Alien = 0,
            Female = 1,
            Male = 2,
            Bear = 3,
            ChemicalMan = 4,
            Chicken = 5,
            Cowboy = 6,
            Hero = 7,
            Hoodie = 8,
            Jester = 9,
            Knight = 10,
            Ninja = 11,
            Robot = 12,
            SalaryMan = 13,
            Samurai = 14,
            Sniper = 15,
            Solider = 16,
            Spaceman = 17,
            Terrorist = 18,
            Veteran = 19,
        }

        public enum BodyType
        {
            Body,
            Head
        }
    }
}