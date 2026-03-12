using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Sequences;
using Sources.Frameworks.DeepFramework.DeepTwens.Sequences.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions
{
    public static class DeepTweenExtension
    {
        public static DeepSequence Sequence(
            LoopType loopType = LoopType.None,
            params Func<CancellationToken, UniTask>[] tasks)
        {
            return new DeepSequence().SetLoopType(loopType).AddRange(tasks);
        }

        public static Tween Move(this Transform transform, Vector3 targetPosition, float duration)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetDuration(duration)
                .SetTask(async token =>
                {
                    float animationTime = 0;
                    Vector3 startPos = transform.position;
                    Vector3 endPos = targetPosition;
                    int animationTimeLength = 1;

                    while (animationTime < animationTimeLength)
                    {
                        animationTime += (Time.deltaTime / duration);
                        float delta = EaseManager.Evaluate(tweener.Ease, animationTime);
                        transform.position = Vector3.Lerp(startPos, endPos, delta);

                        await UniTask.Yield(token);
                    }
                });

            return tweener;
        }

        public static Tween SpriteSwap(this Image image, List<Sprite> sprites, float duration, float delay = 1f)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetDuration(duration)
                .SetTask(async token =>
                {
                    float animationTime = 0f;
                    int currentSpriteIndex = 0;
                    float currentDelay = 0f;

                    while (animationTime <= duration)
                    {
                        animationTime += DeepTweenBrain.DeltaTime;
                        currentDelay += DeepTweenBrain.DeltaTime;

                        if (currentDelay >= delay)
                        {
                            currentDelay = 0f;
                            currentSpriteIndex++;

                            if (currentSpriteIndex >= sprites.Count)
                                currentSpriteIndex = 0;

                            image.sprite = sprites[currentSpriteIndex];
                        }

                        await UniTask.Yield(token);
                    }

                    tweener?.Stop();
                });

            return tweener;
        }

        public static Tween Move(this CharacterController characterController, Vector3 targetPosition,
            float duration)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetTask(async token =>
                {
                    if (characterController == null)
                        return;

                    float temOffset = characterController.stepOffset;
                    characterController.stepOffset = 0.3f;

                    while (characterController != null &&
                           Vector3.Distance(characterController.transform.position, targetPosition) > 0.2f)
                    {
                        // duration -= Time.deltaTime;
                        // float delta = MyMath.Vector3DurationToDelta(characterController.transform.position,
                        //     targetPosition, duration);
                        // Vector3 direction = (targetPosition - characterController.transform.position).normalized *
                        //                     delta;
                        //
                        // characterController.Move(direction);
                        //
                        // await UniTask.Yield(token);
                    }

                    if (characterController == null)
                        return;

                    characterController.stepOffset = temOffset;
                });

            return tweener;
        }

        public static Tween RotateEuler(this Transform transform, Vector3 targetEulerAngles, float duration)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetTask(async token =>
                {
                    while (transform != null &&
                           Quaternion.Angle(transform.rotation, Quaternion.Euler(targetEulerAngles)) > 0.1f)
                    {
                        // duration -= Time.deltaTime;
                        // Quaternion targetAngle = Quaternion.Euler(targetEulerAngles);
                        // float delta = MyMath.QuaternionAnglesDurationToDelta(transform.rotation, targetAngle, duration);
                        //
                        // transform.rotation = Quaternion.RotateTowards(
                        //     transform.rotation,
                        //     targetAngle,
                        //     delta);
                        //
                        // await UniTask.Yield(token);
                    }
                })
                .Play();

            return tweener;
        }

        public static Tween RotateQuaternion(this Transform transform, Quaternion targetAngles, float duration)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetTask(async token =>
                {
                    while (transform != null && Quaternion.Angle(transform.rotation, targetAngles) > 0.1f)
                    {
                        // duration -= Time.deltaTime;
                        // float delta =
                        //     MyMath.QuaternionAnglesDurationToDelta(transform.rotation, targetAngles, duration);
                        //
                        // transform.rotation = Quaternion.RotateTowards(
                        //     transform.rotation,
                        //     targetAngles,
                        //     delta);
                        //
                        // await UniTask.Yield(token);
                    }
                })
                .Play();

            return tweener;
        }

        public static Tween To(Func<float> getter, Action<float> setter, float to, float duration)
        {
            Tween tweener = DeepTweenBrain.GetTweener();
            tweener
                .SetTask(async token =>
                {
                    float animationTime = 0;
                    float from = getter.Invoke();
                    float current = from;
                    var animationTimeLength = 1;

                    while (animationTime < animationTimeLength)
                    {
                        animationTime += (Time.deltaTime / duration);
                        float delta = EaseManager.Evaluate(tweener.Ease, animationTime);
                        current = Mathf.Lerp(from, to, delta);
                        setter?.Invoke(current);

                        await UniTask.Yield(token);
                    }
                })
                .Play();

            return tweener;
        }
    }
}