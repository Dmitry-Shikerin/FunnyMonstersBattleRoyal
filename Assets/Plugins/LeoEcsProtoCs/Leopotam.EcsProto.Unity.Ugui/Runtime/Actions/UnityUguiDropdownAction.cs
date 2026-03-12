// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using TMPro;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Ugui {
    [RequireComponent (typeof (TMP_Dropdown))]
    public sealed class UnityUguiDropdownAction : UnityUguiAction<UnityUguiDropdownChangeEvent> {
        TMP_Dropdown _dropdown;

        void Awake () {
            _dropdown = GetComponent<TMP_Dropdown> ();
            _dropdown.onValueChanged.AddListener (OnDropdownValueChanged);
        }

        public void OnDropdownValueChanged (int v) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = _dropdown;
                msg.Value = v;
            }
        }
    }
}
