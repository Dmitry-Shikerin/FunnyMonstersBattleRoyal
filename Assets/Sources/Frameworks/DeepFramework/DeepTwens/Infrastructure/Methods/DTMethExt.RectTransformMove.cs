using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Methods
{
    public static partial class DTMethExt
    {
        public static async UniTask LocalMove(this RectTransform rectTransform, Vector3 targetPosition, float duration, CancellationToken token, Ease ease = Ease.Linear)
        {
            float animationTime = 0;
            Vector3 startPos = rectTransform.localPosition;
            Vector3 endPos = targetPosition;
            int animationTimeLength = 1;

            while (animationTime < animationTimeLength)
            {
                animationTime += (Time.deltaTime / duration);
                float delta = EaseManager.Evaluate(ease, animationTime);
                rectTransform.localPosition = Vector3.Lerp(startPos, endPos, delta);

                await UniTask.Yield(PlayerLoopTiming.Initialization, token);
            }
        }

        public static async UniTask LocalMove(this RectTransformMoveData data, CancellationToken token)
        {
            Vector3 targetPos = data.TargetTransformPosition != null
                ? data.TargetTransformPosition.position
                : data.TargetPosition;
            await data.Transform.LocalMove(targetPos, data.Duration, token, data.Ease);
        }
    }
}