using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Sequences;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Presentation
{
    public class DeepTweenMonoSequence : MonoBehaviour
    {
        [TabGroup("Settings")]
        [SerializeField] public bool IsLoop;
        [field: TabGroup("Settings")]
        [field: SerializeField] public List<DeepTweenAnimation> Animations { get; private set; }
        [TabGroup("Events")]
        [SerializeField] public UnityEvent OnPlay;
        [TabGroup("Events")]
        [SerializeField] public UnityEvent OnComplete;
        

        public DeepSequence Sequence;

        [Button]
        public void Play()
        {
            Sequence = DeepTweenExtension.Sequence();

            Sequence.OnStart(OnPlay.Invoke);
            
            foreach (DeepTweenAnimation tween in Animations)
                Sequence.Add(() => tween.MonoTween.Play().Play());

            if (IsLoop)
                Sequence.SetLoop();

            Sequence.OnComplete(() =>
            {
                Debug.Log($"Completed");
                OnComplete.Invoke();
            });
            
            Sequence.StartAsync();
        }

        [Button]
        public void Stop()
        {
            Sequence.Stop();
        }
    }
}