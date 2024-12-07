using System;
using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Spawner;
using _Project.Code.UI.View.State;
using _Project.Code.UI.View.State.Gameplay;
using _Project.Code.Utils;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class PauseState : IState
    {
        [Inject] private readonly GameplayStateMachine _fsm;
        [Inject] private readonly EnemySpawner _enemySpawner;
        [Inject] private readonly PauseStateView _view;
        [Inject] private readonly AudioMixerGroup _audioMixerGroup;
        
        public async void Enter()
        {
            _audioMixerGroup.audioMixer.FindSnapshot("Paused").TransitionTo(0.1f);
            Time.timeScale = Constants.Time.PausedValue;
            _enemySpawner.Pause();

            var result = await _view.Open();

            switch (result)
            {
                case TargetStates.Resume:
                    _fsm.EnterPreviousState();
                    break;
                case TargetStates.Restart:
                    _fsm.Enter<RestartState>();
                    Debug.Log("ASD");
                    break;
                case TargetStates.LoadMenu:
                    _fsm.Enter<LoadMenuState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Debug.Log("2");
        }

        public void Exit()
        {           
            _audioMixerGroup.audioMixer.FindSnapshot("Resumed").TransitionTo(0.1f);
            Time.timeScale = Constants.Time.ResumedValue;
            _view.Hide();
        }
    }
}