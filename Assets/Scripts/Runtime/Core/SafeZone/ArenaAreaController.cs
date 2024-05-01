using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.SafeZone
{
    public class ArenaAreaController : MonoBehaviour
    {
        // #region Refs
        // [SerializeField] private ArenaSettings _arenaSettings;
        // public ArenaChestController ArenaChestController { get; private set; }
        // public ArenaCharacterController ArenaCharacterController { get; private set; }
        // private SphereCollider _collider;
        // #endregion
        //
        // #region Internals
        // private bool _preventTimerManipulation = false;
        //
        // private Tween _radiusChangeTween;
        //
        // private float _timerForCall;
        // private float _durationBetweenCalls = 1;
        //
        // private float _currentRadius; public float CurrentRadius => _currentRadius;
        //
        // private int _minute, _second;
        // private ArenaExpandLevel _currentArenaExpandLevel;
        // public ArenaExpandLevel CurrentArenaExpandLevel => _currentArenaExpandLevel;
        // #endregion
        //
        // private void Awake()
        // {
        //     ArenaChestController = GetComponentInParent<ArenaChestController>();
        //     ArenaCharacterController = GetComponentInParent<ArenaCharacterController>();
        //     
        //     _collider = GetComponent<SphereCollider>();
        //     
        //     _currentArenaExpandLevel = new ArenaExpandLevel();
        //     SetCurrentArenaExpandLevel(_arenaSettings.ArenaExpandLevels[0]);
        //     _currentRadius = _currentArenaExpandLevel.LevelRadius;
        //     
        //     _minute = (int)_currentArenaExpandLevel.LevelDuration / 60;
        //     _second = (int) _currentArenaExpandLevel.LevelDuration % 60;
        //     UiManagerSystem.Instance.UiTimer.SetTimerText(_minute,_second);
        // }
        //
        // private void Start()
        // {
        //     SetCurrentRadius(_currentRadius);
        // }
        //
        // private void OnTriggerEnter(Collider other)
        // {
        //     Character character = other.GetComponent<Character>();
        //     if(character == null) return;
        //
        //     if (ArenaCharacterController.CharactersOutRange.Contains(character))
        //     {
        //         ArenaCharacterController.CharactersOutRange.Remove(character);
        //     }
        //     
        //     if (!ArenaCharacterController.CharactersInRange.Contains(character))
        //     {
        //         ArenaCharacterController.CharactersInRange.Add(character);
        //         if (character.GetType() == typeof(Player))
        //         {
        //             UiManagerSystem.Instance.SetDamageIndicatorActiveness(false);
        //             // LevelReferences.Instance.PostProcessManager.SetPostProcessActiveness(0,0.6f, 0);
        //         }
        //     }
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     Character character = other.GetComponent<Character>();
        //     if(character == null) return;
        //
        //     if (ArenaCharacterController.CharactersInRange.Contains(character))
        //     {
        //         ArenaCharacterController.CharactersInRange.Remove(character);
        //     }
        //     
        //     if (!ArenaCharacterController.CharactersOutRange.Contains(character))
        //     {
        //         ArenaCharacterController.CharactersOutRange.Add(character);
        //         
        //         if (character.GetType() == typeof(Player))
        //         {
        //             // LevelReferences.Instance.PostProcessManager.SetPostProcessActiveness(0.1f,0.6f, 0.7f);
        //             UiManagerSystem.Instance.SetDamageIndicatorActiveness(true);
        //         }
        //     }
        // }
        //
        // private void Update()
        // {
        //     if(LevelManager.CurrentLevelState != LevelState.LevelStarted) return;
        //     if(_preventTimerManipulation) return;
        //
        //     if (_timerForCall >= _durationBetweenCalls)
        //     {
        //         _timerForCall = 0;
        //         _minute = (int)_currentArenaExpandLevel.LevelDuration / 60;
        //         _second = (int) _currentArenaExpandLevel.LevelDuration % 60;
        //         UiManagerSystem.Instance.UiTimer.SetTimerText(_minute,_second);
        //     }
        //
        //     _currentArenaExpandLevel.LevelDuration -= Time.deltaTime;
        //     _timerForCall += Time.deltaTime;
        //
        //     if (_currentArenaExpandLevel.LevelDuration <= 0)
        //     {
        //         UiManagerSystem.Instance.UiTimer.SetTimerTextActiveness(false);
        //         UiManagerSystem.Instance.SetWarningTextActiveness(true);
        //         _currentArenaExpandLevel.LevelDuration = 0;
        //         SetPreventTimerManipulation(true);
        //         
        //         if(_currentArenaExpandLevel.LevelNo >= _arenaSettings.ArenaExpandLevels.Count-1) return;
        //         SetArenaExpandLevelAsNext();
        //         
        //         SetCurrentRadius(_currentArenaExpandLevel.LevelRadius,10f, () =>
        //         { 
        //             SetPreventTimerManipulation(false);
        //            UiManagerSystem.Instance.UiTimer.SetTimerTextActiveness(true);
        //            UiManagerSystem.Instance.SetWarningTextActiveness(false);
        //
        //            if (_currentArenaExpandLevel.LevelNo > 0)
        //            {
        //                ArenaChestController.SpawnChestsWhenAreaBeingSmallerProcessFinished(_currentArenaExpandLevel.ChessCount);
        //            }
        //
        //         });
        //     }
        // }
        //
        //
        // public void SetArenaExpandLevelAsNext()
        // {
        //     int newLevel = _currentArenaExpandLevel.LevelNo + 1;
        //
        //     SetCurrentArenaExpandLevel(_arenaSettings.ArenaExpandLevels[newLevel]);
        //     
        // }
        //
        // #region Set Functions
        // public void SetCurrentRadius(float value)
        // {
        //     _currentRadius = value;
        //     transform.localScale = new Vector3(_currentRadius, 3f, _currentRadius);
        //     LevelReferences.Instance.ArenaManager.Ground.SetRadius(_currentRadius);
        // }
        //
        // public void SetCurrentRadius(float value, float duration, Action callback)
        // {
        //     _radiusChangeTween?.Kill();
        //     _radiusChangeTween = DOTween.To(
        //         () => _currentRadius,
        //         x => _currentRadius = x,
        //         value,
        //         duration
        //     )
        //         .SetEase(Ease.Linear)
        //         .OnUpdate(()=>
        //         {
        //             transform.localScale = new Vector3(_currentRadius, 3f, _currentRadius);
        //             LevelReferences.Instance.ArenaManager.Ground.SetRadius(_currentRadius);
        //         })
        //         .OnComplete(()=> callback());
        // }
        //
        // public void SetCurrentArenaExpandLevel(ArenaExpandLevel targetArenaExpandLevel)
        // {
        //     _currentArenaExpandLevel.LevelDuration = targetArenaExpandLevel.LevelDuration;
        //     _currentArenaExpandLevel.LevelNo = targetArenaExpandLevel.LevelNo;
        //     _currentArenaExpandLevel.LevelRadius = targetArenaExpandLevel.LevelRadius;
        //     _currentArenaExpandLevel.ChessCount = targetArenaExpandLevel.ChessCount;
        // }
        //
        // public void SetPreventTimerManipulation(bool value)
        // {
        //     _preventTimerManipulation = value;
        // }
        // #endregion


    }
}