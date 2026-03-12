// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.ConditionalSystems {
    public interface IConditionalSystemSolver {
        bool Solve ();
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class ConditionalSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem, IProtoBenchSystems {
        readonly IConditionalSystemSolver _solver;
        readonly bool _autoInject;
        IProtoSystem[] _nestedSystems;
        ProtoSystems _protoSystems;

        public ConditionalSystem (IConditionalSystemSolver solver, bool autoInject, params IProtoSystem[] nestedSystems) {
#if DEBUG
            if (solver == null) { throw new Exception ($"solver не может быть пустым"); }
            if (nestedSystems == null || nestedSystems.Length == 0) { throw new Exception ($"список систем не может быть пустым"); }
#endif
            _solver = solver;
            _autoInject = autoInject;
            _nestedSystems = nestedSystems;
        }

        public void Init (IProtoSystems systems) {
            _protoSystems = new (systems.World ());
            var services = systems.Services ();
            foreach (var kv in systems.NamedWorlds ()) {
                _protoSystems.AddWorld (kv.Value, kv.Key);
            }
            foreach (var kv in services) {
                _protoSystems.AddService (kv.Value, kv.Key);
            }
            services = _protoSystems.Services ();

            AutoInjectModule.Inject (_solver, _protoSystems, services);
            foreach (var s in _nestedSystems) {
                _protoSystems.AddSystem (s);
                if (_autoInject) {
                    AutoInjectModule.Inject (s, _protoSystems, services);
                }
            }

            _nestedSystems = default;
            _protoSystems.Init ();
        }

        public void Run () {
            if (_solver.Solve ()) {
                _protoSystems.Run ();
            }
        }

        public void Destroy () => _protoSystems.Destroy ();
        public Slice<IProtoSystem> Systems () => _protoSystems.Systems ();
        public int Bench (int idx, ProtoBenchType sType) => _protoSystems.Bench (idx, sType);
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
