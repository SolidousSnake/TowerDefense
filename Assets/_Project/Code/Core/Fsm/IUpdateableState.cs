namespace _Project.Code.Core.Fsm
{
    public interface IUpdateableState : IState
    {
        public void Update();
    }
}