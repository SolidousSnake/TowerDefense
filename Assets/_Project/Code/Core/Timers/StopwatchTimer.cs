namespace _Project.Code.Core.Timers
{
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer() : base(0)
        {
        }

        protected override void Tick(float deltaTime)
        {
            if (IsRunning) 
                Time += deltaTime;
        }

        public void Reset() => Time = 0;

        public float GetTime() => Time;
    }
}