// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Types {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    [StructLayout (LayoutKind.Sequential)]
    public struct Box2f {
        public Vec2f Min;
        public Vec2f Max;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Box2f (in Vec2f min, Vec2f max) {
            Min = min;
            Max = max;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Box2fExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Center (this Box2f lhs) {
            return new ((lhs.Min.X + lhs.Max.X) * 0.5f, (lhs.Min.Y + lhs.Max.Y) * 0.5f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Contains (this Box2f lhs, Vec2f point) {
            return
                lhs.Min.X <= point.X && lhs.Max.X >= point.X &&
                lhs.Min.Y <= point.Y && lhs.Max.Y >= point.Y;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Intersects (this Box2f lhs, Box2f box) {
            return !(
                box.Min.X > lhs.Max.X ||
                box.Min.Y > lhs.Max.Y ||
                box.Max.X < lhs.Min.X ||
                box.Max.Y < lhs.Min.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Box2f AddPoint (this Box2f lhs, Vec2f rhs) {
            return new (lhs.Min.Min (rhs), lhs.Max.Max (rhs));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutAddPoint (this ref Box2f lhs, Vec2f rhs) {
            lhs.Min = lhs.Min.Min (rhs);
            lhs.Max = lhs.Max.Max (rhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Box2f Add (this Box2f lhs, Box2f rhs) {
            return new (lhs.Min.Min (rhs.Min), lhs.Max.Max (rhs.Max));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutAdd (this ref Box2f lhs, Box2f rhs) {
            lhs.Min = lhs.Min.Min (rhs.Min);
            lhs.Max = lhs.Max.Max (rhs.Max);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Box2f lhs, Box2f rhs) {
            return lhs.Min.EqualsTo (rhs.Min) && lhs.Max.EqualsTo (rhs.Max);
        }
    }
}
