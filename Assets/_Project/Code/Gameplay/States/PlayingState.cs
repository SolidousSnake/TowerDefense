using _Project.Code.Core.Fsm;
using _Project.Code.Gameplay.Spawner;
using _Project.Code.Utils;
using UnityEngine;
using VContainer;

namespace _Project.Code.Gameplay.States
{
    public class PlayingState : IState
    {
        [Inject] private readonly EnemySpawner _enemySpawner;
        
        public void Enter()
        {
            Time.timeScale = Constants.Time.ResumedValue;
            _enemySpawner.ResumeOrStart();
        }

        public void Exit()
        {
        }
    }
}