using UnityEngine;

namespace Sources.EcsBoundedContexts.PrivacyPolicys.Presentation
{
    public class PrivacyPolicyView : MonoBehaviour
    {
        //[Required] [SerializeField] private UIButton _button;

        private void OnEnable()
        {
            //_button.onClickEvent.AddListener(OnClick);
        }

        private void OnDisable()
        {
            //_button.onClickEvent.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            //TODO ссылка будет позже
            //Application.OpenURL("");
        }
    }
}