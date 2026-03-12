using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation
{
    public class UiScaler
    {
        private Camera _uiCamera;
        private float designHeight = 2160f;
        private float pixelsPerUnit = 100f;
    
        public void UpdateCameraSize()
        {
            if (_uiCamera.orthographic)
            {
                // Базовый orthographic size для дизайнерского разрешения
                float baseSize = designHeight / (2 * pixelsPerUnit);
            
                // Корректировка для текущего разрешения
                float currentAspect = (float)Screen.width / Screen.height;
                float designAspect = 3840f / 2160f;
            
                if (currentAspect > designAspect)
                {
                    // Широкий экран - используем базовый размер
                    _uiCamera.orthographicSize = baseSize;
                }
                else
                {
                    // Узкий экран - корректируем размер
                    _uiCamera.orthographicSize = baseSize * (designAspect / currentAspect);
                }
            }
        }
    
        public void Initialize(Camera uiCamera)
        {
            _uiCamera = uiCamera;
        }
        
        public void SetResolution()
        {
            
        }
    }
}