using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace _Project.Code.Core.Time
{
    public class Timer
    {
        private readonly TimeSpan _initialTime;
        private CancellationTokenSource _cts;
        private ReactiveProperty<TimeSpan> _elapsedTime;
        
        private bool _paused;
        
        public Timer(TimeSpan initialTime)
        {
            _initialTime = initialTime;
            _elapsedTime = new ReactiveProperty<TimeSpan>(_initialTime);
        }

        public IReadOnlyReactiveProperty<TimeSpan> ElapsedTime => _elapsedTime; 

        private async UniTask Tick()
        {
            var second = TimeSpan.FromSeconds(1);
            while (!_cts.IsCancellationRequested)
            {
                if(_paused)
                    return;
                
                _elapsedTime.Value -= second;

                if (_elapsedTime.Value <= TimeSpan.Zero)
                {
                    _elapsedTime.Value = TimeSpan.Zero;
                    Stop();                      
                }
                
                await UniTask.Delay(second, cancellationToken: _cts.Token); 
            }
        }
        
        public void Start()
        {
            _cts = new CancellationTokenSource();
            _paused = false;
            Reset();
            Tick().Forget();
        }

        public void Stop() => _cts?.Cancel();

        public void Pause() => _paused = true;

        public void Resume() => _paused = false;

        public void Reset()
        {
            _elapsedTime.Value = _initialTime;
            _paused = false;
        }
    }
}