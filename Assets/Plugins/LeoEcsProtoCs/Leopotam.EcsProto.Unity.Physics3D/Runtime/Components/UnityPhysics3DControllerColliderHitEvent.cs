// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEngine;

namespace Leopotam.EcsProto.Unity.Physics3D {
    public struct UnityPhysics3DControllerColliderHitEvent {
        public string SenderName;
        public GameObject Sender;
        public Collider Collider;
        public Vector3 Point;
        public Vector3 Normal;
        public Vector3 RelativeVelocity;
    }
}
