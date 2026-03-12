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
    public struct Box3f {
        public Vec3f Min;
        public Vec3f Max;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Box3f (in Vec3f min, Vec3f max) {
            Min = min;
            Max = max;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Box3fExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Center (this Box3f lhs) {
            return new (
                (lhs.Min.X + lhs.Max.X) * 0.5f,
                (lhs.Min.Y + lhs.Max.Y) * 0.5f,
                (lhs.Min.Z + lhs.Max.Z) * 0.5f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Contains (this Box3f lhs, Vec3f rhs) {
            return lhs.Min.X <= rhs.X && lhs.Max.X >= rhs.X &&
                   lhs.Min.Y <= rhs.Y && lhs.Max.Y >= rhs.Y &&
                   lhs.Min.Z <= rhs.Z && lhs.Max.Z >= rhs.Z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Intersects (this Box3f lhs, Box3f box) {
            return !(
                box.Min.X > lhs.Max.X ||
                box.Min.Y > lhs.Max.Y ||
                box.Min.Z > lhs.Max.Z ||
                box.Max.X < lhs.Min.X ||
                box.Max.Y < lhs.Min.Y ||
                box.Max.Z < lhs.Min.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Box3f AddPoint (this Box3f lhs, Vec3f point) {
            return new (lhs.Min.Min (point), lhs.Max.Max (point));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutAddPoint (this ref Box3f lhs, Vec3f point) {
            lhs.Min = lhs.Min.Min (point);
            lhs.Max = lhs.Max.Max (point);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Box3f Add (this Box3f lhs, Box3f box) {
            return new (lhs.Min.Min (box.Min), lhs.Max.Max (box.Max));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutAdd (this ref Box3f lhs, Box3f box) {
            lhs.Min = lhs.Min.Min (box.Min);
            lhs.Max = lhs.Max.Max (box.Max);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Box3f lhs, Box3f rhs) {
            return lhs.Min.EqualsTo (rhs.Min) && lhs.Max.EqualsTo (rhs.Max);
        }
    }
}
