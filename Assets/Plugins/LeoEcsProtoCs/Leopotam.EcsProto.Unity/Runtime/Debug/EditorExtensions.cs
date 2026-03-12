// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

#if UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace Leopotam.EcsProto.Unity {
    public static class EditorExtensions {
        static readonly Dictionary<Type, string> _namesCache = new ();

        public static string CleanTypeNameCached (Type type) {
            if (!_namesCache.TryGetValue (type, out var name)) {
                name = DebugHelpers.CleanTypeName (type);
                _namesCache[type] = name;
            }
            return name;
        }
    }
}
#endif
