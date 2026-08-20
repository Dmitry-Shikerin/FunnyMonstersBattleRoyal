using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Data;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Methods;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Implementation
{
    public class CurtainView : MonoBehaviour, ICurtainView
    {
        public const string AssetPath = "DeepFramework/CurtainView";
        
        [Required] [SerializeField] private CanvasGroup _canvasGroup;
        [Title("Fade")]
        [SerializeField] private CanvasGroupFadeData _downCanvasGroupFadeData;
        [SerializeField] private CanvasGroupFadeData _upCanvasGroupFadeData;
        [SerializeField] private float _fadeUpDelay = 1f;
        [Title("Move")]
        [SerializeField] private RectTransformMoveData _downRectTransformMoveData;
        [SerializeField] private RectTransformMoveData _upRectTransformMoveData;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public bool IsInProgress { get; private set; }

        private void Awake()
        {
            Hide();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDestroy() =>
            _cancellationTokenSource.Cancel();

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
            //DeepSoundManager.Play(SoundDatabaseName.UiSounds, SoundName.ShowCurtain);
            CancellationToken token = _cancellationTokenSource.Token;
            IsInProgress = true;
            Show();
            await UniTask.WhenAll(
                _downRectTransformMoveData.LocalMove(token),
                _downCanvasGroupFadeData.Fade(token));
        }

        public async UniTask HideAsync()
        {
            CancellationToken token = _cancellationTokenSource.Token;
            await UniTask.WhenAll(
                _upRectTransformMoveData.LocalMove(token),
                UpFade(_upCanvasGroupFadeData, token));
            Hide();
            IsInProgress = false;
        }

        private async UniTask UpFade(CanvasGroupFadeData data, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeUpDelay), cancellationToken: token);
            await data.Fade(token);
        }
    }
}