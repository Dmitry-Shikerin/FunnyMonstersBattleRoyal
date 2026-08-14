using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Glove
{
    public class GloveSkinChangerUiView : MonoBehaviour
    {
        [Required] [SerializeField] private UiButton _leftButton;
        [Required] [SerializeField] private UiButton _rightButton;
        [Required] [SerializeField] private TMP_Text _nameText;

        private GloveSkinChangerView _view;

        public void Construct(GloveSkinChangerView view)
        {
            gameObject.SetActive(false);
            _view = view;
            _nameText.text = _view.CurrentSkinName.ToString();
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _leftButton.AddOnClickListener(_view.SetPreviousSkin_Rpc);
            _rightButton.AddOnClickListener(_view.SetNextSkin_Rpc);
        }

        protected void OnDisable()
        {
            _leftButton.RemoveOnClickListener(_view.SetPreviousSkin_Rpc);
            _rightButton.RemoveOnClickListener(_view.SetNextSkin_Rpc);
        }

        public void SetText(string text)
        {
            _nameText.text = text;
        }
    }
}