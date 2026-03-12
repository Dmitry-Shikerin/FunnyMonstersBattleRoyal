// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using UnityEditor;

namespace Leopotam.EcsProto.Unity.Editor {
    [CustomEditor (typeof (ProtoEntityDebugView))]
    sealed class ProtoEntityDebugViewInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI () 
        {
            var observer = (ProtoEntityDebugView) target;
            
            var view = new EntityDebugInfo 
            {
                World = observer.World,
                Entity = observer.Entity,
                System = observer.DebugSystem,
            };
            ComponentInspectors.RenderEntity (view);
            EditorUtility.SetDirty (target);
        }
    }
}
