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
    public struct Flat {
        public Vec3f Normal;
        public float Distance;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Flat (in Vec3f inNormal, Vec3f inPoint) {
            Normal = inNormal;
            Distance = -Normal.Dot (inPoint);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Flat (in Vec3f inNormal, float d) {
            Normal = inNormal;
            Distance = d;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Flat (in Vec3f a, Vec3f b, Vec3f c) {
            Normal = b.Sub (a).Cross (c.Sub (a)).Norm ();
            Distance = -Normal.Dot (a);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class FlatExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Flat Neg (this Flat lhs) {
            return new (lhs.Normal.Neg (), -lhs.Distance);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ProjectPoint (this Flat lhs, Vec3f point) {
            return point.Sub (lhs.Normal.Scale (lhs.Normal.Dot (point) + lhs.Distance));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float DistanceToPoint (this Flat lhs, Vec3f point) {
            return lhs.Normal.Dot (point) + lhs.Distance;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool ArePointsOnSameSide (this Flat lhs, Vec3f a, Vec3f b) {
            var d0 = lhs.Normal.Dot (a) + lhs.Distance;
            var d1 = lhs.Normal.Dot (b) + lhs.Distance;
            return (d0 > 0f && d1 > 0f) || (d0 <= 0f && d1 <= 0f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static (float, bool) Raycast (this Flat lhs, Beam3 beam) {
            var dot = beam.Direction.Dot (lhs.Normal);
            if (dot * dot < MathFast.Epsilon) {
                return (default, false);
            }
            var dist = (-beam.Origin.Dot (lhs.Normal) - lhs.Distance) / dot;
            return (dist, dist > 0f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Flat lhs, Flat rhs) {
            return
                lhs.Normal.EqualsTo (rhs.Normal) &&
                (lhs.Distance - rhs.Distance) * (lhs.Distance - rhs.Distance) < MathFast.Epsilon;
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Plane ToPlane (this Flat lhs) {
            return new UnityEngine.Plane (lhs.Normal.ToVector3 (), lhs.Distance);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Flat ToFlat (this UnityEngine.Plane lhs) {
            return new (lhs.normal.ToVec3f (), lhs.distance);
        }
#endif
    }
}
