using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Body
{
    public class BodySkinChangerUiView : MonoBehaviour
    {
        [Required] [SerializeField] private UiButton _leftButton;
        [Required] [SerializeField] private UiButton _rightButton;
        [Required] [SerializeField] private TMP_Text _nameText;
        
        private BodySkinChangerView _view;

        public void Construct(BodySkinChangerView view)
        {
            gameObject.SetActive(false);
            _view = view;
            _nameText.text = _view.CurrentSkinName.ToString();
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            if (_view == null)
                return;
            
            _leftButton.AddOnClickListener(_view.SetPreviousSkin);
            _rightButton.AddOnClickListener(_view.SetNextSkin);
        }

        protected void OnDisable()
        {
            if (_view == null)
                return;
            
            _leftButton.RemoveOnClickListener(_view.SetPreviousSkin);
            _rightButton.RemoveOnClickListener(_view.SetNextSkin);
        }

        public void SetText(string text)
        {
            _nameText.text = text;
        }
    }
}