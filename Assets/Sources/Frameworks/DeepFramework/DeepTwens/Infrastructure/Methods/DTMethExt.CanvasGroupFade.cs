using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Methods
{
    public static partial class DTMethExt
    {
        public static async UniTask Fade(this CanvasGroup canvasGroup, float target, float duration, CancellationToken token, Ease ease = Ease.Linear)
        {
            float animationTime = 0;
            float startPos = canvasGroup.alpha;
            int animationTimeLength = 1;

            while (animationTime < animationTimeLength)
            {
                animationTime += (Time.deltaTime / duration);
                float delta = EaseManager.Evaluate(ease, animationTime);
                canvasGroup.alpha= Mathf.Lerp(startPos, target, delta);

                await UniTask.Yield(PlayerLoopTiming.Initialization, token);
            }
        }

        public static async UniTask Fade(this CanvasGroupFadeData data, CancellationToken token) =>
            await data.CanvasGroup.Fade(data.Target, data.Duration, token, data.Ease);
    }
}