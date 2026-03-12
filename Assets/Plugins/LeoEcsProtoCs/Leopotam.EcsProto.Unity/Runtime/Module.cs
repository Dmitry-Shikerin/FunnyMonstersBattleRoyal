// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

#if ENABLE_IL2CPP
using System;
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Unity {
    public sealed class UnityModule : IProtoModule 
    {
        readonly string _updatePointName;
        readonly bool _disableDebugSystems;
        readonly string _systemsName;
        readonly bool _bakeComponentsInName;
        readonly string _entityNameFormat;

        public UnityModule (bool disableDebugSystems = default, string systemsName = default, bool bakeComponentsInName = true, string entityNameFormat = "D6", string updatePointName = default) {
            _updatePointName = updatePointName;
            _disableDebugSystems = disableDebugSystems;
            _systemsName = systemsName;
            _bakeComponentsInName = bakeComponentsInName;
            _entityNameFormat = entityNameFormat;
        }

        public void Init (IProtoSystems systems) 
        {
            systems
                .AddSystem (new UnityWorldsSystem ())
                .AddSystem (new UnityLinkSystem ());
#if UNITY_EDITOR
            if (!_disableDebugSystems) 
            {
                systems.AddSystem (new ProtoSystemsDebugSystem (_systemsName), _updatePointName);
                systems.AddSystem (new ProtoWorldDebugSystem (default, _bakeComponentsInName, _entityNameFormat), _updatePointName);
                
                foreach (var kv in systems.NamedWorlds ()) 
                {
                    systems.AddSystem (new ProtoWorldDebugSystem (kv.Key, _bakeComponentsInName, _entityNameFormat), _updatePointName);
                }
            }
#endif
        }

        public IProtoAspect[] Aspects () => null;
        public IProtoModule[] Modules () => null;
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
