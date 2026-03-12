// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.EcsProto.Unity.Ugui {
    [RequireComponent (typeof (Slider))]
    public sealed class UnityUguiSliderAction : UnityUguiAction<UnityUguiSliderChangeEvent> {
        Slider _slider;

        void Awake () {
            _slider = GetComponent<Slider> ();
            _slider.onValueChanged.AddListener (OnSliderValueChanged);
        }

        public void OnSliderValueChanged (float v) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = _slider;
                msg.Value = v;
            }
        }
    }
}
