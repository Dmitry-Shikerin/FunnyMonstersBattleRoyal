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
    public struct Beam3 {
        public Vec3f Origin;
        public Vec3f Direction;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Beam3 (Vec3f origin, Vec3f direction) {
            Origin = origin;
            Direction = direction;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Beam3Extensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f PointAt (this Beam3 lhs, float distance) {
            return new (
                lhs.Origin.X + lhs.Direction.X * distance,
                lhs.Origin.Y + lhs.Direction.Y * distance,
                lhs.Origin.Z + lhs.Direction.Z * distance);
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Ray ToRay (this Beam3 lhs) {
            return new UnityEngine.Ray (lhs.Origin.ToVector3 (), lhs.Direction.ToVector3 ());
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Beam3 ToBeam3 (this UnityEngine.Ray v) {
            return new (v.origin.ToVec3f (), v.direction.ToVec3f ());
        }
#endif
    }
}
