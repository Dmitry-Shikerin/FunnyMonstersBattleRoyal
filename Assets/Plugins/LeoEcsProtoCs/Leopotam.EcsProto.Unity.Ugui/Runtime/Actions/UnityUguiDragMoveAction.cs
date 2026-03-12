// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine.EventSystems;

namespace Leopotam.EcsProto.Unity.Ugui {
    public sealed class UnityUguiDragMoveAction : UnityUguiAction<UnityUguiDragMoveEvent>, IDragHandler {
        public void OnDrag (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref NewEvent ();
                msg.SenderName = SenderName ();
                msg.Sender = Sender ();
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Delta = eventData.delta;
                msg.Button = eventData.button;
            }
        }
    }
}
