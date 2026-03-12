// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Editor {
    [CustomEditor (typeof (ProtoSystemsDebugView))]
    sealed class ProtoSystemsDebugViewInspector : UnityEditor.Editor {
        static bool _initOpened;
        static bool _runOpened;
        static bool _destroyOpened;
        static Dictionary<int, string> _formattedTime;

        static string[] _labels = {
            "Init системы",
            "Run системы",
            "Destroy системы"
        };

        static Type[] _ifaces = {
            typeof(IProtoInitSystem),
            typeof(IProtoRunSystem),
            typeof(IProtoDestroySystem)
        };

        const string NoDataLabel = "<???>";

        public override void OnInspectorGUI () {
            var view = (ProtoSystemsDebugView) target;
            var savedState = GUI.enabled;
            GUI.enabled = true;
            RenderCategory (ProtoBenchType.Init, view.Systems, ref _initOpened);
            RenderCategory (ProtoBenchType.Run, view.Systems, ref _runOpened);
            RenderCategory (ProtoBenchType.Destroy, view.Systems, ref _destroyOpened);
            GUI.enabled = savedState;
            EditorUtility.SetDirty (target);
        }

        void RenderCategory (ProtoBenchType sysType, ProtoSystems systems, ref bool opened) {
            opened = EditorGUILayout.BeginFoldoutHeaderGroup (opened, _labels[(int) sysType]);
            if (opened) {
                RenderLabeledList (sysType, systems);
            }
            EditorGUILayout.EndFoldoutHeaderGroup ();
            EditorGUILayout.Space ();
        }

        void RenderLabeledList (ProtoBenchType sysType, IProtoBenchSystems systems) {
            EditorGUI.indentLevel++;
            var savedWidth = EditorGUIUtility.labelWidth;
            var list = systems.Systems ();
            for (var i = 0; i < list.Len (); i++) {
                var item = list.Get (i);
                var itemType = item.GetType ();
                if (_ifaces[(int) sysType].IsAssignableFrom (itemType)) {
                    var itemName = EditorExtensions.CleanTypeNameCached (type: item.GetType ());
#if DEBUG || LEOECSPROTO_SYSTEM_BENCHES
                    var time = systems.Bench (i, sysType);
                    EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 84f;
                    EditorGUILayout.LabelField (itemName, FormattedTime (time));
#else
                    EditorGUILayout.LabelField (sysName, NoDataLabel);
#endif
                }
                if (item is IProtoBenchSystems benchSystems) {
                    RenderLabeledList (sysType, benchSystems);
                }
            }
            EditorGUIUtility.labelWidth = savedWidth;
            EditorGUI.indentLevel--;
        }

        static string FormattedTime (int time) {
            if (time < 0) {
                return NoDataLabel;
            }
            _formattedTime ??= new Dictionary<int, string> (512);
            if (!_formattedTime.TryGetValue (time, out var timeStr)) {
                timeStr = $"{time * 0.01f:F2}мс";
                _formattedTime[time] = timeStr;
            }
            return timeStr;
        }
    }
}
