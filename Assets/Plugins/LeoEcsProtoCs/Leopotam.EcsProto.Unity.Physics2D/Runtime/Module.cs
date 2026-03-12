// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using System;
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Unity.Physics2D {
    public sealed class UnityPhysics2DModule : IProtoModule {
        readonly string _worldName;
        readonly string _clearPointName;

        public UnityPhysics2DModule (string worldName = default, string clearPointName = default) {
            _worldName = worldName;
            _clearPointName = clearPointName;
        }

        public void Init (IProtoSystems systems) {
            systems
                .DelHere<UnityPhysics2DCollisionEnterEvent> (_worldName, _clearPointName)
                .DelHere<UnityPhysics2DCollisionExitEvent> (_worldName, _clearPointName)
                .DelHere<UnityPhysics2DTriggerEnterEvent> (_worldName, _clearPointName)
                .DelHere<UnityPhysics2DTriggerExitEvent> (_worldName, _clearPointName);
        }

        public IProtoAspect[] Aspects () {
            return _worldName != null ? null : new IProtoAspect[] { new UnityPhysics2DAspect () };
        }
        public IProtoModule[] Modules () => null;
    }

    public sealed class UnityPhysics2DAspect : ProtoAspectInject {
        public readonly ProtoPool<UnityPhysics2DCollisionEnterEvent> CollisionEnterEvent;
        public readonly ProtoPool<UnityPhysics2DCollisionExitEvent> CollisionExitEvent;
        public readonly ProtoPool<UnityPhysics2DTriggerEnterEvent> TriggerEnterEvent;
        public readonly ProtoPool<UnityPhysics2DTriggerExitEvent> TriggerExitEvent;
    }
}

#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices {
    enum Option {
        NullChecks = 1,
        ArrayBoundsChecks = 2
    }

    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute (Option option, object value) { Option = option; Value = value; }
    }
}
#endif
