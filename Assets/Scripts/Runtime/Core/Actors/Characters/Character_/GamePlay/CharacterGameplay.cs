using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterLineRenderer;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterSprite;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Team_;
using Assets.Scripts.Runtime.Core.User_;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay
{
    public class CharacterGameplay : Character, ICharacterGameplay
    {
        #region CACHE
        public bool IsOwner { get; protected set; }
        public bool IsAlive { get; protected set; }
        public User User { get; protected set; }
        public Team Team { get; protected set; }
        public TeamScene TeamScene { get; protected set; }
        public TeamScene CounterTeamScene { get; protected set; }

        protected TeamSpawnPoint _teamSpawnPoint;
        public bool IsSameTeamWithOwner { get; protected set; }
        #endregion


        #region DI REF
        public IDamagable Damagable { get; protected set; }
        public ICharacterWeaponAttack CharacterWeaponAttack { get; protected set; }

        public ICharacterAttack Attackable { get; protected set; }
        public IMovableAndRotatable MovableAndRotatable { get; protected set; }
        public IAnimatorUser AnimatorUser { get; protected set; }

        protected ControllerMatch _controllerMatch;
        public CharacterAttackStateMachine CharacterAttackStateMachine { get; protected set; }
        protected CharacterSprites _characterSprites;
        public CharacterLineRenderer CharacterLineRenderer { get; protected set; }
        #endregion


        #region DIRECT REF
        [Space(20)]
        [Header("Character Gameplay")]
        [SerializeField] protected TMP_Text _nameText;
        [SerializeField] protected List<GameObject> ActiveObjects;
        [SerializeField] protected Collider _characterCollider;
        #endregion


        [Inject]
        public void Construct(ControllerMatch controllerMatch, IDamagable damagable,
           ICharacterAttack attackable, IMovableAndRotatable movableAndRotatable, IAnimatorUser animatorUser,
            ICharacterWeaponAttack characterWeaponAttack, CharacterAttackStateMachine characterAttackStateMachine,
            CharacterSprites characterSprites, CharacterLineRenderer characterLineRenderer)
        {
            _controllerMatch = controllerMatch;
            Damagable = damagable;
            Attackable = attackable;
            MovableAndRotatable = movableAndRotatable;
            AnimatorUser = animatorUser;
            CharacterWeaponAttack = characterWeaponAttack;
            CharacterAttackStateMachine = characterAttackStateMachine;
            _characterSprites = characterSprites;
            CharacterLineRenderer = characterLineRenderer;
        }


        public void InitCharacter()
        {
            SetIsAlive(true);

            SetOwnerShip();
            SetIsSameTeamWithOwner();

            InitCharacterVisual();
            StartCoroutine(InitCharacterWeapon());

            InitCharacterDamagable();
            InitUserName();
            InitAttackState();
            InitSprites();
        }



        public virtual void Revive()
        {
            transform.position = _teamSpawnPoint.transform.position;
            transform.rotation = Quaternion.Euler(_teamSpawnPoint.ForwardRotationAngle);

            SetIsAlive(true);
            Damagable.SetHealth(Damagable.GetInitHealth(), null);
            Attackable.ResetAmmo();

            InitAttackState();
        }

        #region INIT FUNCTIONS
        protected void InitSprites()
        {
            if (IsOwner)
            {
                _characterSprites.SetRadialSpriteColor(_characterSprites.OwnerColor);
            }
            else if (!IsOwner)
            {
                if (IsSameTeamWithOwner)
                {
                    _characterSprites.SetRadialSpriteColor(_characterSprites.SameTeamColor);
                }
                else if (!IsSameTeamWithOwner)
                {
                    _characterSprites.SetRadialSpriteColor(_characterSprites.CounterTeamColor);
                }
            }
        }
        protected void InitAttackState()
        {
            CharacterAttackStateMachine.SetCurrentState(CharacterAttackStateMachine.GetInitialState());
        }
        protected void InitUserName()
        {
            _nameText.text = User.Userdata.UserName;
        }

        protected void InitCharacterVisual()
        {
            var characterVisualType = User.Userdata.CharacterVisualType;
            CharacterVisual.SetActiveCharacterVisualParts(characterVisualType);
        }
        protected IEnumerator InitCharacterWeapon()
        {

            var weaponType = User.Userdata.WeaponType;
            CharacterWeapon.SetActiveWeapon(weaponType);
            while (CharacterWeapon.GetActiveWeapon() == null)
            {
                yield return null;
            }

            var weaponAttack = CharacterWeapon.GetActiveWeapon().GetComponent<WeaponAttack>();
            weaponAttack.SetWeaponType(weaponType);
            CharacterWeaponAttack.SetActiveWeaponAttack(weaponAttack);
            CharacterWeaponAttack.InitChararcterWeaponAttack();
        }

        protected void InitCharacterDamagable()
        {
            Damagable.SetHealth(Damagable.GetHealth(), null);
        }
        #endregion


        #region SET FUNCTIONS
        public virtual void SetIsAlive(bool isAlive)
        {
            IsAlive = isAlive;

            ActiveObjects.ForEach(x => x.SetActive(isAlive));
            _characterCollider.enabled = isAlive;
            MovableAndRotatable.SetPreventMovementAndRotation(!isAlive);
        }
        public void SetOwnerShip()
        {
            IsOwner = User.Userdata.UserId == _managerSave.SaveSystem.SaveState.UserId;
        }
        public void SetUser(User user)
        {
            User = user;
        }

        public void SetTeam(Team team)
        {
            Team = team;
        }

        public void SetTeamSpawnPoint(TeamSpawnPoint teamSpawnPoint)
        {
            _teamSpawnPoint = teamSpawnPoint;
        }

        public void SetTeamScene(TeamScene teamScene)
        {
            TeamScene = teamScene;
        }

        public void SetCounterTeamScene(TeamScene teamScene)
        {
            CounterTeamScene = teamScene;
        }

        public void SetIsSameTeamWithOwner()
        {
            IsSameTeamWithOwner = _controllerMatch.OwnerCharacterGameplay.TeamScene == TeamScene;
        }
        #endregion

        #region TEST HELPERS
        public void SetMovambleAndRotatable(IMovableAndRotatable movableAndRotatable)
        {
            MovableAndRotatable = movableAndRotatable;
        }

        public bool GetIsAlive()
        {
            return IsAlive;
        }

        public bool GetIsOwner()
        {
            return IsOwner;
        }

        public User GetUser()
        {
            return User;
        }

        public Team GetTeam()
        {
            return Team;
        }

        public TeamScene GetTeamScene()
        {
            return TeamScene;
        }
        #endregion

    }
}