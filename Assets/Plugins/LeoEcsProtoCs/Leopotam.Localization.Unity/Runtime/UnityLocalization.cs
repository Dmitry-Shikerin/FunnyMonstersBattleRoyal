// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;

namespace Leopotam.Localization.Unity {
    public static class UnityLocalization {
        static Localization _l;

        public static void Set (Localization l) {
            _l = l;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Localization Get () {
            return _l;
        }
    }
}
