using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Sequences.Handlers;
using Sources.Frameworks.DeepFramework.DeepTwens.Sequences.Types;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Sequences
{
    public class DeepSequence : ISequence
    {
        private MySequenceLoopTaskHandler _mySequenceLoopTaskHandler = new ();
        private CancellationTokenSource _token = new ();
        public List<Func<CancellationToken, UniTask>> Tasks { get; } = new ();

        private LoopType _loopType = LoopType.None;
        private bool _isComplete;
        private bool _isStarted;
        private bool _isPlaying;
        private SequencePool _pool;

        public event Action Completed;
        public event Action Started;
        public event Action Playing;

        public bool IsStarted
        {
            get => _isStarted;
            set
            {
                _isStarted = value;

                if (_isStarted)
                    Started?.Invoke();
            }
        }

        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                _isComplete = value;

                if (_isComplete)
                {
                    Completed?.Invoke();
                    _token.Cancel();
                }
            }
        }       
        
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;

                if (_isPlaying)
                    Playing?.Invoke();
            }
        }

        public DeepSequence Add(Func<CancellationToken, UniTask> task)
        {
            Tasks.Add(task);
            return this;
        }

        public DeepSequence Add(Action action)
        {
            Tasks.Add(async _ => action.Invoke());
            return this;
        }

        public DeepSequence AddRange(params Func<CancellationToken, UniTask>[] tasks)
        {
            Tasks.AddRange(tasks);
            return this;
        }

        public DeepSequence Add(DeepSequence sequence)
        {
            Tasks.Add(_ =>
            {
                UniTask task = sequence.StartAsync(_token);
                sequence.IsStarted = false;
                sequence.IsComplete = false;
                return task;
            });
            return this;
        }

        public DeepSequence AddDelayFromSeconds(float seconds)
        {
            Tasks.Add(token => UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: token));
            return this;
        }

        public async UniTask StartAsync(CancellationTokenSource cancellationToken = default)
        {
            _token = cancellationToken ?? new CancellationTokenSource();

            try
            {
                IsStarted = true;
                IsPlaying = true;
                
                await StartSequence(
                    async token =>
                    {
                        foreach (Func<CancellationToken, UniTask> task in Tasks) 
                            await task.Invoke(token);
                    });
                
                IsPlaying = false;
                IsComplete = true;
            }
            catch (OperationCanceledException)
            {
            }
        }

        public DeepSequence SetPool(SequencePool pool)
        {
            _pool = pool;
            return this;
        }

        public DeepSequence SetLoop()
        {
            _loopType = LoopType.Loop;
            return this;
        }

        public DeepSequence OnComplete(Action action)
        {
            Completed = action;
            return this;
        }

        public DeepSequence OnStart(Action action)
        {
            Started = action;
            return this;
        }

        public DeepSequence SetLoopType(LoopType loopType)
        {
            _loopType = loopType;
            return this;
        }

        public void Stop()
        {
            _token.Cancel();
        }

        private UniTask StartSequence(Func<CancellationToken, UniTask> task)
        {
            return _loopType switch
            {
                LoopType.None => _mySequenceLoopTaskHandler.FromNumber(_token.Token, task, 1),
                LoopType.Loop => _mySequenceLoopTaskHandler.FromLoop(_token.Token, task),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}