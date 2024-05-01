using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Runtime.Tests
{
    public class TestPlayerMovement
    {
        [UnityTest]
        public IEnumerator CharacterGameplayPlayerMovement_WhenUpInput_ExpectTransformPositionZIsHigher_Test()
        {
            //ARRANGE 
            GameObject playerGO = new GameObject("Player");
            CharacterMovementPlayer characterMovementPlayer = playerGO.AddComponent<CharacterMovementPlayer>();

            Rigidbody rigidbody = playerGO.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;

            CapsuleCollider capsuleCollider = playerGO.AddComponent<CapsuleCollider>();

            characterMovementPlayer.Test_SetRigidbody(rigidbody);
            characterMovementPlayer.Test_SetMockCharacterControllerSettings();

            //ACT
            float inputDuration = 0.5f;
            Vector3 upInput = new Vector3(0, 1, 0);
            while (inputDuration > 0)
            {
                inputDuration -= Time.deltaTime;
                characterMovementPlayer.Move(upInput);
                yield return null;
            }

            yield return new WaitForSeconds(inputDuration);

            //ASSERT
            Assert.IsTrue(playerGO.transform.position.z > 0f);
        }
    }
}