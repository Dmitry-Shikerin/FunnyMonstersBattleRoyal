using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Tweners;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners
{
    public class Tween : IDisposable
    {
        private Func<CancellationToken, UniTask> _task;
        private CancellationTokenSource _token;
        private TweenPool _pool;

        public Tween()
        {
            Reset();
        }

        public bool IsPlaying { get; private set; }

        public UniTask Task { get; private set; }
        public Ease Ease { get; private set; }
        public float Duration { get; private set; }
        public float TimeDelta { get; private set; }

        public Tween Play()
        {
            Task = Handle();
            return this;
        }

        private async UniTask Handle()
        {
            try
            {
                CancellationTokenSource token = GetToken();
                IsPlaying = true;
                await _task.Invoke(token.Token);
                IsPlaying = false;
                _pool?.Return(this);
            }
            catch (OperationCanceledException)
            {
                _pool?.Return(this);
            }
        }

        public Tween SetTask(Func<CancellationToken, UniTask> task)
        {
            _task = task;
            return this;
        }

        public Tween SetToken(CancellationTokenSource token)
        {
            _token = token;
            return this;
        }

        public void Dispose()
        {
            Reset();
        }

        public Tween SetPool(TweenPool tweenPool)
        {
            _pool = tweenPool;
            return this;
        }

        public Tween SetEase(Ease ease)
        {
            Ease = ease;
            return this;
        }

        public Tween SetDuration(float duration)
        {
            Duration = duration;
            return this;
        }
        
        public void Reset()
        {
            _token?.Cancel();
            _task = null;
            Ease = Ease.Linear;
        }
        
        public Tween SetTimeDelta(float timeDelta)
        {
            TimeDelta = timeDelta;
            return this;
        }

        private CancellationTokenSource GetToken()
        {
            CancellationTokenSource token = _token ?? new CancellationTokenSource();

            if (token.IsCancellationRequested)
                _token = new CancellationTokenSource();

            return _token ?? new CancellationTokenSource();
        }

        public void Stop()
        {
            _token?.Cancel();
        }
    }
}