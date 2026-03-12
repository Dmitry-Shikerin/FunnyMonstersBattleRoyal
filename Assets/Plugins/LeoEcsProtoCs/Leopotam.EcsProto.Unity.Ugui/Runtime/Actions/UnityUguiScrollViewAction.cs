// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.EcsProto.Unity.Ugui {
    [RequireComponent (typeof (ScrollRect))]
    public sealed class UnityUguiScrollViewAction : UnityUguiAction<UnityUguiScrollViewEvent> {
        ScrollRect _scrollView;

        void Awake () {
            _scrollView = GetComponent<ScrollRect> ();
            _scrollView.onValueChanged.AddListener (OnScrollViewValueChanged);
        }

        public void OnScrollViewValueChanged (Vector2 v) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = _scrollView;
                msg.Value = v;
            }
        }
    }
}
