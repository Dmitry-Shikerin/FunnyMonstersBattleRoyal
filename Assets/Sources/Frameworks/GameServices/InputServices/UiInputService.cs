using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Frameworks.GameServices.InputServices
{
    public class UiInputService : MonoBehaviour, IUiInputService, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float _minSwipeDistance = 50f;
        [SerializeField] private float _dubleSwipeDelay = 0.5f;

        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        public InputDirection InputDirection { get; private set; }

        private CancellationTokenSource _token;

        private async void StartTimer()
        {
            _token?.Cancel();
            _token = new CancellationTokenSource();
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_dubleSwipeDelay), cancellationToken: _token.Token);

                InputDirection = InputDirection.Default;
                Debug.Log($"Stop swipe");
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startTouchPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _endTouchPosition = eventData.position;
            CheckSwipe();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _endTouchPosition = eventData.position;
            //CheckSwipe();
            // CheckDoubleVertical();
            // StartTimer();
            InputDirection = InputDirection.Default;
        }

        private void CheckSwipe()
        {
            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

            if (swipeDelta.magnitude < _minSwipeDistance)
                return;

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                CheckHorizontal(swipeDelta);

                return;
            }

            CheckVertical(swipeDelta);
        }

        private void CheckHorizontal(Vector2 swipeDelta)
        {
            if (swipeDelta.x > 0)
            {
                Debug.Log("Свайп вправо");
                InputDirection = InputDirection.Right;

                return;
            }

            Debug.Log("Свайп влево");
            InputDirection = InputDirection.Left;
        }

        private void CheckVertical(Vector2 swipeDelta)
        {
            if (swipeDelta.y > 0)
            {
                Debug.Log("Свайп вверх");
                InputDirection = InputDirection.Up;

                return;
            }

            Debug.Log("Свайп вниз");
            InputDirection = InputDirection.Down;
        }

        private void CheckDoubleVertical()
        {
            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

            if (swipeDelta.magnitude < _minSwipeDistance)
                return;

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                return;
            
            if (swipeDelta.y > 0)
            {
                if (InputDirection != InputDirection.Up)
                    return;
                
                Debug.Log("Дабл Свайп вверх");
                InputDirection = InputDirection.DoubleUp;

                return;
            }

            if (InputDirection != InputDirection.Down)
                return;
            
            Debug.Log("Дабл Свайп вниз");
            InputDirection = InputDirection.DoubleDown;
        }
    }
}