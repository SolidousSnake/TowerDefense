namespace _Project.Code.Core.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}