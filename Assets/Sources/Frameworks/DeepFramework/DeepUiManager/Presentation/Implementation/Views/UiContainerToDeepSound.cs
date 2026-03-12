using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DeepSoundSender))]
    public class UiContainerToDeepSound : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Container To Deep Sound</i></color></b></size>";
        private const int Space = 10;

        //Fields
        [DisplayAsString(false)] [HideLabel] [SerializeField]
        private string _label = Label;

        [SerializeField] private UiContainerBase _container;
        [SerializeField] private DeepSoundSender _showSender;
        [SerializeField] private DeepSoundSender _hideSender;

        private void Start()
        {
            if (_container != null && _showSender != null)
                _container.Showed += _showSender.Play;

            if (_container != null && _hideSender != null)
                _container.Hided += _hideSender.Play;
        }

        private void OnDestroy()
        {
            if (_container != null && _showSender != null)
                _container.Showed -= _showSender.Play;

            if (_container != null && _hideSender != null)
                _container.Hided -= _hideSender.Play;
        }


        [OnInspectorInit]
        private void Init()
        {
            if (_container == null)
                _container = GetComponent<UiContainerBase>();
        }
    }
}