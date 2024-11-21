using _Project.Code.Core.Fsm;
using UnityEngine;

namespace _Project.Code.Gameplay.States
{
    public class FailureState : IState
    {
        public void Enter()
        {
            Debug.Log("Fail");
        }

        public void Exit()
        {
        }
    }
}