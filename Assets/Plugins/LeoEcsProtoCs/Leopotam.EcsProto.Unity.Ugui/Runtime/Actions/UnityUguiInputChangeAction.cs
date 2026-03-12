// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using TMPro;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Ugui {
    [RequireComponent (typeof (TMP_InputField))]
    public sealed class UnityUguiInputChangeAction : UnityUguiAction<UnityUguiInputChangeEvent> {
        TMP_InputField _input;

        void Awake () {
            _input = GetComponent<TMP_InputField> ();
            _input.onValueChanged.AddListener (OnInputValueChanged);
        }

        public void OnInputValueChanged (string v) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = _input;
                msg.Value = v;
            }
        }
    }
}
