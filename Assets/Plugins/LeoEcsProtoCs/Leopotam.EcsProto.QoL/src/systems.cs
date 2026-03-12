// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Collections.Generic;
using System.Reflection;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.QoL {
    public class ProtoModules {
        readonly List<IProtoModule> _modules;
        readonly List<IProtoAspect> _aspects;

        public ProtoModules (params IProtoModule[] modules) {
            _aspects = new ();
            _modules = new (modules?.Length ?? 4);
            if (modules != null) {
                foreach (var mod in modules) {
                    AddModule (mod);
                }
            }
        }

        public ProtoModules AddModule (IProtoModule module) {
#if DEBUG
            if (module == null) { throw new Exception ("экземпляр модуля должен существовать"); }
#endif
            _modules.Add (module);
            var subMods = module.Modules ();
            if (subMods != null) {
                foreach (var subMod in subMods) {
                    AddModule (subMod);
                }
            }
            return this;
        }

        public ProtoModules AddAspect (IProtoAspect aspect) {
            _aspects.Add (aspect);
            return this;
        }

        public IProtoAspect BuildAspect () {
            return new ComposedAspect (_modules, _aspects);
        }

        public IProtoModule BuildModule () {
            return new ComposedModule (_modules);
        }

        sealed class ComposedAspect : IProtoAspect {
            readonly List<IProtoModule> _modules;
            readonly List<IProtoAspect> _aspects;

            public ComposedAspect (List<IProtoModule> modules, List<IProtoAspect> aspects) {
                _modules = modules;
                _aspects = aspects;
            }

            public void Init (ProtoWorld world) {
                foreach (var mod in _modules) {
                    var aspects = mod.Aspects ();
                    if (aspects != null) {
                        foreach (var aspect in aspects) {
                            _aspects.Add (aspect);
                        }
                    }
                }
                foreach (var aspect in _aspects) {
                    aspect.Init (world);
                }
            }

            public void PostInit () {
                foreach (var aspect in _aspects) {
                    aspect.PostInit ();
                }
            }
        }

        sealed class ComposedModule : IProtoModule {
            readonly List<IProtoModule> _modules;

            public ComposedModule (List<IProtoModule> modules) {
                _modules = modules;
            }

            public void Init (IProtoSystems systems) {
                foreach (var mod in _modules) {
                    systems.AddModule (mod);
                }
            }

            public IProtoAspect[] Aspects () => null;
            public IProtoModule[] Modules () => null;
        }

        public void Init (IProtoSystems systems) {
            systems.AddModule (BuildModule ());
        }

        public IProtoModule[] Modules () => _modules.ToArray ();
    }
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class AutoInjectModule : IProtoModule {
        static readonly Type _diAttrType = typeof (DIAttribute);
        static readonly Type _aspectType = typeof (IProtoAspect);
        static readonly Type _itType = typeof (IProtoIt);
        static readonly Type _worldType = typeof (ProtoWorld);
        readonly bool _injectToServices;

        public AutoInjectModule (bool injectToServices = false) => _injectToServices = injectToServices;

        public void Init (IProtoSystems systems) {
            systems.AddSystem (new AutoInjectSystem (_injectToServices));
        }

        public IProtoAspect[] Aspects () => null;
        public IProtoModule[] Modules () => null;
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        sealed class AutoInjectSystem : IProtoInitSystem {
            readonly bool _injectToServices;

            public AutoInjectSystem (bool injectToServices) => _injectToServices = injectToServices;

            public void Init (IProtoSystems systems) {
                var services = systems.Services ();
                var allSystems = systems.Systems ();
                for (int i = 0, iMax = allSystems.Len (); i < iMax; i++) {
                    Inject (allSystems.Get (i), systems, services);
                }
                if (_injectToServices) {
                    foreach (var kv in services) {
                        Inject (kv.Value, systems, services);
                    }
                }
            }
        }

        public static void Inject (object target, IProtoSystems systems, Dictionary<Type, object> services) {
#if DEBUG
            if (target == null) { throw new Exception ("экземпляр объекта для инъекций в его поля не существует"); }
#endif
            foreach (var fi in target.GetType ().GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (fi.IsStatic) { continue; }
                if (Attribute.IsDefined (fi, _diAttrType)) {
                    var worldName = ((DIAttribute) Attribute.GetCustomAttribute (fi, _diAttrType)).WorldName;
                    // аспекты.
                    if (_aspectType.IsAssignableFrom (fi.FieldType)) {
                        fi.SetValue (target, systems.World (worldName).Aspect (fi.FieldType));
                        continue;
                    }
                    // итераторы.
                    if (_itType.IsAssignableFrom (fi.FieldType)) {
                        var it = (IProtoIt) fi.GetValue (target);
#if DEBUG
                        if (it == null) { throw new Exception ($"итератор \"{fi.Name}\" в \"{DebugHelpers.CleanTypeName (target.GetType ())}\" должен быть создан заранее"); }
#endif
                        var world = systems.World (worldName);
                        fi.SetValue (target, it.Init (world));
                        continue;
                    }
                    // миры.
                    if (_worldType.IsAssignableFrom (fi.FieldType)) {
                        fi.SetValue (target, systems.World (worldName));
                        continue;
                    }
                    // сервисы.
                    if (services.TryGetValue (fi.FieldType, out var injectObj)) {
                        fi.SetValue (target, injectObj);
                    } else {
#if DEBUG
                        throw new Exception ($"ошибка инъекции пользовательских данных в \"{DebugHelpers.CleanTypeName (target.GetType ())}\" - тип поля \"{fi.Name}\" отсутствует в списке сервисов");
#endif
                    }
                }
            }
        }
    }
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class DelHereSystem<T> : IProtoInitSystem, IProtoRunSystem where T : struct {
        readonly string _worldName;
        ProtoIt _it;
        ProtoPool<T> _pool;

        public DelHereSystem (string worldName) {
            _worldName = worldName;
        }

        public void Init (IProtoSystems systems) {
            var world = systems.World (_worldName);
            var t = typeof (T);
            _pool = (ProtoPool<T>) world.Pool (t);
            _it = new (new[] { t });
            _it.Init (world);
        }

        public void Run () {
            foreach (var e in _it) {
                _pool.Del (e);
            }
        }
    }

    public interface IProtoInitService {
        void Init (IProtoSystems systems);
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InitHereSystem<T> : IProtoInitSystem where T : class, IProtoInitService {
        public void Init (IProtoSystems systems) {
#if DEBUG
            if (!systems.Services ().TryGetValue (typeof (T), out var svc) || svc == null) {
                throw new Exception ($"ошибка инициализации \"{DebugHelpers.CleanTypeName (typeof (T))}\" - тип отсутствует в списке сервисов");
            }
#endif
            ((IProtoInitService) systems.Services ()[typeof (T)]).Init (systems);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class ProtoSystemsExtensions {
        public static IProtoSystems AddServices (this IProtoSystems systems, params object[] services) {
            services ??= Array.Empty<object> ();
            foreach (var obj in services) {
                systems.AddService (obj);
            }
            return systems;
        }

        public static IProtoSystems DelHere<T> (this IProtoSystems systems, string worldName = default, string pointName = default)
            where T : struct {
            return systems.AddSystem (new DelHereSystem<T> (worldName), pointName);
        }

        public static IProtoSystems InitHere<T> (this IProtoSystems systems, string pointName = default)
            where T : class, IProtoInitService {
            return systems.AddSystem (new InitHereSystem<T> (), pointName);
        }
    }

    public class DIAttribute : Attribute {
        public readonly string WorldName;

        public DIAttribute () : this (default) { }

        public DIAttribute (string worldName) {
            WorldName = worldName;
        }
    }
}
