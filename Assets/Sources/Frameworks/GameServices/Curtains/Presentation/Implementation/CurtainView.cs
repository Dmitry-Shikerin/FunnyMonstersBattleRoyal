using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using Sources.Frameworks.DeepFramework.DeepTwens.Presentation.Concrete;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.GameServices.Curtains.Presentation.Implementation
{
    public class CurtainView : MonoBehaviour, ICurtainView
    {
        [Required] [SerializeField] private CanvasGroup _canvasGroup;
        [Title("Fade")]
        [Required] [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private float _fadeDownDuration = 1f;
        [SerializeField] private float _fadeUpDuration = 1f;
        [SerializeField] private float _fadeUpDelay = 1f;
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        [Title("Move")]
        [Required] [SerializeField] private RectTransform _backgroundRectTransform;
        [SerializeField] private float _moveDuration = 1.5f;
        [SerializeField] private Ease _moveDownEase = Ease.Linear;
        [SerializeField] private Ease _moveUpEase = Ease.Linear;
        [Required] [SerializeField] private Vector3 _startPosition = new(0, 1080, 0);
        [Required] [SerializeField] private Vector3 _centerPosition = Vector3.zero;
        [Title("Sprites")]
        [Required] [SerializeField] private CanvasGroup _rotateImageCanvasGroup;
        [Required] [SerializeField] private SpriteSwapAnimation _spriteSwapAnimation;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public bool IsInProgress { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _canvasGroup.alpha = 0;
        }

        private void OnEnable() =>
            _cancellationTokenSource = new CancellationTokenSource();
        
        private void OnDisable() =>
            _cancellationTokenSource.Cancel();

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask ShowAsync()
        {
            DeepSoundManager.Play(SoundDatabaseName.UiSounds, SoundName.ShowCurtain);
            IsInProgress = true;
            Show();
            _spriteSwapAnimation.Play();
            await UniTask.WhenAll(
                Move(_centerPosition, _moveDuration, _moveDownEase), 
                DownFade(_fadeCanvasGroup),
                DownFade(_rotateImageCanvasGroup));
        }

        public async UniTask HideAsync()
        {
            await UniTask.WhenAll(
                Move(_startPosition, _moveDuration, _moveUpEase), 
                UpFade(_fadeCanvasGroup),
                UpFade(_rotateImageCanvasGroup));
            Hide();
            IsInProgress = false;
        }

        private async UniTask DownFade(CanvasGroup canvasGroup)
        {
            await Fade(canvasGroup, 1, _fadeDownDuration);
        }    
        
        private async UniTask UpFade(CanvasGroup canvasGroup)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeUpDelay));
            await Fade(canvasGroup, 0, _fadeUpDuration);
        }

        private async UniTask Move(Vector3 target, float duration, Ease ease)
        {
            float animationTime = 0;
            Vector3 startPos = _backgroundRectTransform.localPosition;
            Vector3 endPos = target;
            int animationTimeLength = 1;

            while (animationTime < animationTimeLength)
            {
                animationTime += (Time.deltaTime / duration);
                float delta = EaseManager.Evaluate(ease, animationTime);
                _backgroundRectTransform.localPosition = Vector3.Lerp(startPos, endPos, delta);

                await UniTask.Yield(PlayerLoopTiming.Initialization, _cancellationTokenSource.Token);
            }
        }    
        
        private async UniTask Fade(CanvasGroup canvasGroup, float target, float duration)
        {
            float animationTime = 0;
            float startPos = canvasGroup.alpha;
            int animationTimeLength = 1;

            while (animationTime < animationTimeLength)
            {
                animationTime += (Time.deltaTime / duration);
                float delta = EaseManager.Evaluate(_fadeEase, animationTime);
                canvasGroup.alpha= Mathf.Lerp(startPos, target, delta);

                await UniTask.Yield(PlayerLoopTiming.Initialization, _cancellationTokenSource.Token);
            }
        }
    }
}