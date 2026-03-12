// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Parenting {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    struct ParentingParent : IProtoAutoReset<ParentingParent> {
        public Slice<ProtoPackedEntity> Children;
        public ProtoPool<ParentingChild> ChildrenPool;
        public bool KeepChildrenOnDead;
        public readonly void AutoReset (ref ParentingParent c) {
            c.Children ??= new (8);
            if (c.ChildrenPool != null) {
                if (!c.KeepChildrenOnDead) {
                    var w = c.ChildrenPool.World ();
                    for (var i = c.Children.Len () - 1; i >= 0; i--) {
                        if (c.Children.Get (i).Unpack (w, out var childE)) {
                            // чистка требуется для возможности вызова Parenting-апи
                            // в возможных пользовательских AutoReset-реакциях.
                            c.ChildrenPool.Get (childE).Parent = default;
                            w.DelEntity (childE);
                        }
                    }
                }
                c.ChildrenPool = default;
            }
            c.Children.Clear (false);
            c.KeepChildrenOnDead = false;
        }
    }

    struct ParentingChild {
        public ProtoPackedEntity Parent;
    }
}
