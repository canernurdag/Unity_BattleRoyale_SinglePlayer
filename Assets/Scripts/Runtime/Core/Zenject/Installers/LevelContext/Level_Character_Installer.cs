using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAiLogic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterCollisionAndTrigger;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterHealth;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterLineRenderer;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterParticle;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterSprite;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Interfaces.Weapons;
using Assets.Scripts.Runtime.Core.Zenject.AddressablesAdapters;
using Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer;
using Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Character_Installer : CharacterInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CapsuleCollider>()
              .FromComponentSibling()
              .AsTransient()
              .WhenInjectedInto<CharacterCollisionAndTrigger>();


            Container.Bind<IDamagable>()
                .To<CharacterHealth>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Character>();

            Container.Bind<ICharacterAttack>()
                .To<CharacterAttackBase>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Character>();

            Container.Bind<CharacterAttackBase>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackStateMachine>();

            Container.Bind<CharacterAttackPlayer>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container.Bind<CharacterAnimator>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackStateMachine>();

            Container.Bind<Animator>()
                .FromComponentInChildren()
                .AsTransient()
                .WhenInjectedInto<CharacterAnimator>();

            Container.Bind<IMovableAndRotatable>()
                .To<CharacterMovementAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplayAi>();


            Container.Bind<CharacterMovementAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplayAi>();

            Container.Bind<IMovableAndRotatable>()
                .To<CharacterMovementPlayer>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplayPlayer>();

            Container.Bind<IAnimatorUser>()
                .To<CharacterAnimator>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Character>();

            Container.Bind<ICharacterVisual>()
                .To<CharacterVisual>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Character>();

            Container.Bind<ICharacterWeapon>()
                .To<CharacterWeapon>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Character>();

            Container.Bind<CharacterWeapon>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackBase>();

            Container.Bind<CharacterWeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackBase>();


            Container.Bind<SkinnedMeshRenderer>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterVisualPart>();

            Container.Bind<CharacterVisualPart>()
                 .FromComponentsInChildren()
                 .AsTransient()
                 .WhenInjectedInto<CharacterVisual>();

            Container.Bind<CharacterGameplay>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackStateMachine>();


            Container.Bind<CharacterAttackStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAnimator>();

            Container.Bind<IProvideVelocityValue>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAnimator>();

            Container.Bind<NavMeshAgent>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementAi>();

            Container.Bind<NavMeshAgent>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiMovementStateMachine>();

            Container.Bind<CharacterMovementAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiMovementStateMachine>();


            Container.Bind<CharacterWeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovement>();

            Container.Bind<CharacterGameplay>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovement>();

            Container.Bind<CharacterGameplay>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterHealthUi>();

            Container.Bind<CharacterAiMovementStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementAi>();


            Container.Bind<CharacterAttackStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackAi>();


            Container.Bind<CharacterAttackStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackPlayer>();

            Container.Bind<CharacterMovementPlayer>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackPlayer>();

            Container.Bind<Character>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackBase>();


            Container.Bind<CharacterWeaponAttack>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackAnimationEvents>();

            Container.Bind<CharacterAttackStateMachine>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackAnimationEvents>();

            Container.Bind<CharacterAttackBase>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementPlayer>();



            Container.Bind<AddressableAdapterWeapon>()
                .FromComponentInChildren()
                .AsTransient()
                .WhenInjectedInto<CharacterWeapon>();


            Container.Bind<CharacterGameplay>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterWeaponAttack>();



            Container.Bind<JoystickInputAdaptorPlayerAttack>()
                .FromComponentInHierarchy()
                .AsTransient()
                .WhenInjectedInto<CharacterPlayerAttackStateMachine>();


            Container.Bind<CharacterPlayerAttackStateMachine>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<CharacterHealthUi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterHealth>();

            Container.Bind<NavMeshAgent>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplayAi>();

            Container.Bind<IWeaponAttack>()
                .To<WeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<Weapon>();

            Container.Bind<Weapon>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<WeaponAttack>();

            Container.Bind<Character>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<WeaponAttack>();


            Container.Bind<WeaponTarget>()
                .FromComponentsInChildren(true)
                .AsTransient()
                .WhenInjectedInto<WeaponAimIndicator>();

            Container.Bind<Level_MemoryPool_Controller>()
                .FromComponentInHierarchy()
                .AsCached()
                .WhenInjectedInto<WeaponAttack>();

            Container.Bind<CharacterAttackAmmo>()
                .FromComponentsInChildren()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackBase>();

            Container.Bind<CharacterGameplay>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterHealth>();

            Container.Bind<CharacterParticles>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterHealth>();

            Container.Bind<CharacterAttackBase>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<CharacterAttackAmmo>();

            Container.Bind<ControllerMatch>()
                .FromComponentInHierarchy()
                .AsTransient()
                .WhenInjectedInto<CharacterHealth>();


            Container.Bind<ICharacterWeaponAttack>()
                .To<CharacterWeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplay>();

            Container.Bind<CharacterLineRenderer>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplay>();


            Container.Bind<Rigidbody>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementPlayer>();

            Container.Bind<CharacterGameplayAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiLogic>();

            Container.Bind<CharacterMovementAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiLogic>();

            Container.Bind<CharacterPlayerMovementStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementPlayer>();

            Container.Bind<CharacterPlayerAttackStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementPlayer>();

            Container.Bind<IDamagable>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiLogic>();

            Container.Bind<ICharacterWeaponAttack>()
                .To<CharacterWeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiLogic>();

            Container.Bind<ICharacterWeaponAttack>()
                .To<CharacterWeaponAttack>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterMovementPlayer>();

            Container.Bind<CharacterAttackAi>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterAiLogic>();

            Container.Bind<CharacterMovement>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplay>();

            Container.Bind<CharacterAttackStateMachine>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplay>();

            Container.Bind<CharacterSprites>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<CharacterGameplay>();


            Container.Bind<WeaponAttack>()
                .FromComponentInParents()
                .AsTransient()
                .WhenInjectedInto<WeaponInteractionMelee>();

            Container.Bind<CharacterWeapon>()
               .FromComponentInParents()
               .AsTransient()
               .WhenInjectedInto<AddressableAdapterWeapon>();

            Container.Bind<WeaponAimIndicator>()
                .FromComponentsInChildren(true)
                .AsTransient()
                .WhenInjectedInto<CharacterWeaponAttack>();

        }
    }
}