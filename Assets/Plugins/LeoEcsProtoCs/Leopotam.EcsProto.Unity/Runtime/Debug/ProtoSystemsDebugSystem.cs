// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

#if UNITY_EDITOR
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Leopotam.EcsProto.Unity {
    public sealed class ProtoSystemsDebugView : MonoBehaviour 
    {
        [NonSerialized] public ProtoSystems Systems;
    }

    public sealed class ProtoSystemsDebugSystem : IProtoInitSystem, IProtoDestroySystem 
    {
        readonly string _systemsName;
        GameObject _go;

        public ProtoSystemsDebugSystem (string systemsName = default) 
        {
            _systemsName = systemsName;
        }

        public void Init (IProtoSystems systems) 
        {
            _go = new GameObject (_systemsName != null ? $"[PROTO-SYSTEMS {_systemsName}]" : "[PROTO-SYSTEMS]");
            Object.DontDestroyOnLoad (_go);
            _go.hideFlags = HideFlags.NotEditable;
            var view = _go.AddComponent<ProtoSystemsDebugView> ();
            view.Systems = systems as ProtoSystems;
        }

        public void Destroy () 
        {
            if (Application.isPlaying) 
            {
                Object.Destroy (_go);
            } 
            else 
            {
                Object.DestroyImmediate (_go);
            }
            
            _go = null;
        }
    }
}
#endif
