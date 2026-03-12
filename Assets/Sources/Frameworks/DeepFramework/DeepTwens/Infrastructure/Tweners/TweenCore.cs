using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Tweners;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners
{
    public abstract class TweenCore<T1, T2> : Tween
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
    {
        // private Func<CancellationToken, UniTask> _task;
        // private CancellationTokenSource _token;
        // private TweenPool _pool;
        //
        // public TweenCore()
        // {
        //     Reset();
        // }
        //
        // public Action<T1> Getter;
        // public Action<T1> Setter;
        //
        // public T2 StartValue;
        // public T2 EndValue;
        // public T2 ChangeValue;
        //
        // public bool IsPlaying { get; private set; }
        //
        // public UniTask Task { get; private set; }
        // public Ease Ease { get; private set; }
        // public float Duration { get; private set; }
        // public float TimeDelta { get; private set; }
        //
        // public Tween Play()
        // {
        //     Task = Handle();
        //     return this;
        // }
        //
        // private async UniTask Handle(T1 from, T1 to, float duration)
        // {
        //     try
        //     {
        //         CancellationTokenSource token = GetToken();
        //         IsPlaying = true;
        //
        //         //await _task.Invoke(token.Token);
        //         float animationTime = 0;
        //         T1 current = from;
        //         var animationTimeLength = 1;
        //
        //         while (EndValue.Equals(StartValue) == false && token.IsCancellationRequested == false)
        //         {
        //             while (animationTime < animationTimeLength)
        //             {
        //                 animationTime += (Time.deltaTime / duration);
        //                 float delta = EaseManager.Evaluate(Ease, animationTime);
        //                 current = Mathf.Lerp(from, to, delta);
        //                 Setter?.Invoke(current);
        //
        //                 await UniTask.Delay(TimeSpan.FromSeconds(DeepTweenBrain.DeltaTime));
        //             }
        //         }
        //
        //         IsPlaying = false;
        //         _pool?.Return(this);
        //     }
        //     catch (OperationCanceledException)
        //     {
        //         _pool?.Return(this);
        //     }
        // }
        //
        // protected abstract T2 Lerp(T2 from, T2 to, float delta);
        //
        // public Tween SetTask(Func<CancellationToken, UniTask> task)
        // {
        //     _task = task;
        //     return this;
        // }
        //
        // public Tween SetToken(CancellationTokenSource token)
        // {
        //     _token = token;
        //     return this;
        // }
        //
        // public void Dispose()
        // {
        //     Reset();
        // }
        //
        // public Tween SetPool(TweenPool tweenPool)
        // {
        //     _pool = tweenPool;
        //     return this;
        // }
        //
        // public Tween SetEase(Ease ease)
        // {
        //     Ease = ease;
        //     return this;
        // }
        //
        // public Tween SetDuration(float duration)
        // {
        //     Duration = duration;
        //     return this;
        // }
        //
        // public void Reset()
        // {
        //     _token?.Cancel();
        //     _task = null;
        //     Ease = Ease.Linear;
        // }
        //
        // public Tween SetTimeDelta(float timeDelta)
        // {
        //     TimeDelta = timeDelta;
        //     return this;
        // }
        //
        // private CancellationTokenSource GetToken()
        // {
        //     CancellationTokenSource token = _token ?? new CancellationTokenSource();
        //
        //     if (token.IsCancellationRequested)
        //         _token = new CancellationTokenSource();
        //
        //     return _token ?? new CancellationTokenSource();
        // }
        //
        // public void Stop()
        // {
        //     _token?.Cancel();
        // }
    }
}