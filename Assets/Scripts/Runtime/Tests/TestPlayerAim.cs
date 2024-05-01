using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Runtime.Tests
{
    public class TestPlayerAim
    {
        [UnityTest]
        public IEnumerator CharacterGameplayPlayerRotation_WhenRightInput_ExpectAngleWithVector3RightIsLess()
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
            characterMovementPlayer.transform.rotation = Quaternion.identity;
            float initAngle = Vector3.Angle(characterMovementPlayer.transform.forward, Vector3.right);
            float lastAngle = 0;


            float inputDuration = 0.5f;
            Vector3 upInput = new Vector3(1, 0, 0);

            while (inputDuration > 0)
            {
                inputDuration -= Time.deltaTime;
                characterMovementPlayer.RotateForAttack(upInput);
                lastAngle = Vector3.Angle(characterMovementPlayer.transform.forward, Vector3.right);
                yield return null;
            }

            yield return new WaitForSeconds(inputDuration);

            //ASSERT
            Assert.Less(lastAngle, initAngle);
        }

    }
}