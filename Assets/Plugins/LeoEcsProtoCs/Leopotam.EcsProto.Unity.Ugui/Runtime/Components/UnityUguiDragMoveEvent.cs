// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;
using UnityEngine.EventSystems;

namespace Leopotam.EcsProto.Unity.Ugui {
    public struct UnityUguiDragMoveEvent {
        public string SenderName;
        public GameObject Sender;
        public Vector2 Position;
        public int PointerId;
        public Vector2 Delta;
        public PointerEventData.InputButton Button;
    }
}
