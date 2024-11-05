namespace _Project.Code.Core.StateMachine
{
    public interface IUpdateableState : IState
    {
        public void Update();
    }
}