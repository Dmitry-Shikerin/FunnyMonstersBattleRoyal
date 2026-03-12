// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using Leopotam.EcsProto.QoL;
#if ENABLE_IL2CPP
using System;
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Unity.Ugui {
    public sealed class UnityUguiModule : IProtoModule {
        readonly string _worldName;
        readonly string _clearPointName;

        public UnityUguiModule (string worldName = default, string clearPointName = default) {
            _worldName = worldName;
            _clearPointName = clearPointName;
        }

        public void Init (IProtoSystems systems) {
            systems
                .DelHere<UnityUguiClickEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDownEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDragEndEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDragMoveEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDragStartEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDropEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiEnterEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiExitEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiScrollViewEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiSliderChangeEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiDropdownChangeEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiInputChangeEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiInputEndEvent> (_worldName, _clearPointName)
                .DelHere<UnityUguiUpEvent> (_worldName, _clearPointName);
        }

        public IProtoAspect[] Aspects () {
            return _worldName != null ? null : new IProtoAspect[] { new UnityUguiAspect () };
        }

        public IProtoModule[] Modules () => null;
    }

    public sealed class UnityUguiAspect : ProtoAspectInject {
        public readonly ProtoPool<UnityUguiClickEvent> ClickEvent;
        public readonly ProtoPool<UnityUguiDownEvent> DownEvent;
        public readonly ProtoPool<UnityUguiDragEndEvent> DragEndEvent;
        public readonly ProtoPool<UnityUguiDragMoveEvent> DragMoveEvent;
        public readonly ProtoPool<UnityUguiDragStartEvent> DragStartEvent;
        public readonly ProtoPool<UnityUguiDropEvent> DropEvent;
        public readonly ProtoPool<UnityUguiEnterEvent> EnterEvent;
        public readonly ProtoPool<UnityUguiExitEvent> ExitEvent;
        public readonly ProtoPool<UnityUguiScrollViewEvent> ScrollViewEvent;
        public readonly ProtoPool<UnityUguiSliderChangeEvent> SliderChangeEvent;
        public readonly ProtoPool<UnityUguiDropdownChangeEvent> DropdownChangeEvent;
        public readonly ProtoPool<UnityUguiInputChangeEvent> InputChangeEvent;
        public readonly ProtoPool<UnityUguiInputEndEvent> InputEndEvent;
        public readonly ProtoPool<UnityUguiUpEvent> UpEvent;
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
