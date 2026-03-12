using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public class UiToggleSpriteSwapper : MonoBehaviour
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Toggle Sprite Swaper</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private Button _button;
        [SerializeField] private UiTab _controller;
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;

        private void Awake()
        {
            OnStateChanged(_controller.State);
            _controller.StateChanged += OnStateChanged;
        }

        private void OnDestroy() =>
            _controller.StateChanged -= OnStateChanged;

        private void OnStateChanged(EnableState state) =>
            _targetImage.sprite = state == EnableState.On ? _enabledSprite : _disabledSprite;
    }
}