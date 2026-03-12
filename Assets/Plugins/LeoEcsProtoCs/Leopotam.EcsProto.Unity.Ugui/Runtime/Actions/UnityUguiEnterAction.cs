// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine.EventSystems;

namespace Leopotam.EcsProto.Unity.Ugui {
    public sealed class UnityUguiEnterAction : UnityUguiAction<UnityUguiEnterEvent>, IPointerEnterHandler {
        public void OnPointerEnter (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = Sender ();
            }
        }
    }
}
