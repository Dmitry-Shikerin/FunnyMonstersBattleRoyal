using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(DeepSoundSender))]
    [DisallowMultipleComponent]
    public class ButtonToDeepSound : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Button To Deep Sound</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private Button _button;
        [SerializeField] private DeepSoundSender _sender;

        private void OnEnable() =>
            _button.onClick.AddListener(_sender.Play);

        private void OnDisable() =>
            _button.onClick.RemoveListener(_sender.Play);

        [OnInspectorInit]
        private void SetButton()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_sender == null)
                _sender = GetComponent<DeepSoundSender>();
        }
    }
}