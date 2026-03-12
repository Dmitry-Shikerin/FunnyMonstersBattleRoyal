// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;
using UnityEngine.EventSystems;

namespace Leopotam.EcsProto.Unity.Ugui {
    public struct UnityUguiUpEvent {
        public string SenderName;
        public GameObject Sender;
        public Vector2 Position;
        public int PointerId;
        public PointerEventData.InputButton Button;
    }
}
