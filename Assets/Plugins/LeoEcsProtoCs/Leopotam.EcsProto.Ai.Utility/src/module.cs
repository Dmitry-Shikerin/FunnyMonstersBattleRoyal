// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Ai.Utility {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class AiUtilityModule : IProtoModule {
        readonly string _worldName;
        readonly string _pointName;
        readonly IAiUtilitySolver[] _solvers;

        public AiUtilityModule (string worldName, string pointName, params IAiUtilitySolver[] solvers) {
            _worldName = worldName;
            _pointName = pointName;
            _solvers = solvers ?? Array.Empty<IAiUtilitySolver> ();
        }

        public void Init (IProtoSystems systems) {
            var services = systems.Services ();
            foreach (var solver in _solvers) {
                AutoInjectModule.Inject (solver, systems, services);
            }
            systems.DelHere<AiUtilityResponseEvent> (_worldName, _pointName);
            systems.AddSystem (new AiUtilitySystem (_solvers), _pointName);
        }

        public IProtoAspect[] Aspects () {
            return _worldName != null ? null : new IProtoAspect[] { new AiUtilityAspect () };
        }

        public IProtoModule[] Modules () => null;
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class AiUtilityAspect : ProtoAspectInject {
        internal readonly ProtoPool<AiUtilityRequestEvent> RequestEvent = default;
        internal ProtoIt RequestIt = new (new[] { typeof (AiUtilityRequestEvent) });

        public readonly ProtoPool<AiUtilityResponseEvent> ResponseEvent = default;
        public readonly ProtoIt ResponseIt = new (new[] { typeof (AiUtilityResponseEvent) });

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Request (ProtoEntity entity) {
            if (!RequestEvent.Has (entity)) {
                RequestEvent.Add (entity);
            }
        }
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
