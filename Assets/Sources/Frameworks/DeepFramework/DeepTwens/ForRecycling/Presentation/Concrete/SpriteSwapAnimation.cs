using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation.Concrete
{
    public class SpriteSwapAnimation : MonoBehaviour
    {
        [field: Required] [field: SerializeField] public Image Image { get; private set; }
        [field: Required] [field: SerializeField] public List<Sprite> Sprites { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Delay { get; private set; } = 1f;

        private CancellationTokenSource _token;
        public UniTask Task;

        private void Awake()
        {
            _token = new CancellationTokenSource();
        }

        private void OnDestroy()
        {
            Stop();
        }

        public void Play()
        {
            Stop();
            Task = PlayAsync();
        }

        public async UniTask PlayAsync()
        {
            try
            {
                _token = new CancellationTokenSource();
                float animationTime = 0f;
                int currentSpriteIndex = 0;
                float currentDelay = 0f;

                while (animationTime <= Duration)
                {
                    animationTime += DeepTweenBrain.DeltaTime;
                    currentDelay += DeepTweenBrain.DeltaTime;                    

                    if (currentDelay >= Delay)
                    {
                        currentDelay = 0f;
                        currentSpriteIndex++;

                        if (currentSpriteIndex >= Sprites.Count)
                            currentSpriteIndex = 0;

                        Image.sprite = Sprites[currentSpriteIndex];
                    }

                    await UniTask.Yield(PlayerLoopTiming.Initialization, _token.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void Stop()
        {
            _token?.Cancel();
        }
    }
}