using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterHealth;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Runtime.Tests
{
    public class TestCharacterHealth
    {
        [Test]
        public void CharacterHealth_WhenGet50Damage_ExpectMitigateCurrentHealthBy50()
        {
            //ARRANGE 
            GameObject playerGO = new GameObject("Player");
            CharacterHealth characterHealth = playerGO.AddComponent<CharacterHealth>();

            ICharacterGameplay characterGameplay = Substitute.For<ICharacterGameplay>();
            characterGameplay.GetIsAlive().Returns(true);
            characterGameplay.GetIsOwner().Returns(false);
            characterHealth.Test_SetCurrentHealth(120);

            ICharacterGameplay attacherCharacterGameplay = Substitute.For<ICharacterGameplay>();

            characterHealth.Test_SetCharacterGameplay(characterGameplay);
            var currentHealth = characterHealth.Test_GetCurrentHealth();
            var damageAmount = 50;
            var newHealth = currentHealth - damageAmount;

            //ACT
            characterHealth.SetHealth(newHealth, attacherCharacterGameplay);

            //ASSERT
            Assert.AreEqual(characterHealth.Test_GetCurrentHealth(), 70);
        }
    }
}