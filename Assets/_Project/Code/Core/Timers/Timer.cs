using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Core.Timers
{
    public abstract class Timer
    {
        private CancellationTokenSource _cts;
        protected float InitialTime { get; set; }
        protected float Time { get; set; }
        protected bool IsRunning { get; private set; }
        
        public float Progress => Time / InitialTime;

        protected Timer(float value)
        {
            InitialTime = value;
        }

        public Action OnStart { get; set; }
        public Action OnFinish { get; set; }

        public virtual void Start()
        {
            Time = InitialTime;
            if (IsRunning) 
                return;
            
            _cts = new CancellationTokenSource();
            IsRunning = true;
            OnStart?.Invoke();
            
            Tick().Forget();
        }

        public virtual void Stop()
        {
            if (!IsRunning) 
                return;
            _cts.Cancel();
            IsRunning = false;
            OnFinish?.Invoke();
        }

        public void SetTime(float time) => InitialTime = time;

        protected abstract void Tick(float deltaTime);

        private async UniTask Tick()
        {
            while (!_cts.IsCancellationRequested)
            {
                Tick(UnityEngine.Time.deltaTime);
                await UniTask.DelayFrame(1);
            }
        }
    }
}