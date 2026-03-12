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
    public struct Vec3i {
        public int X;
        public int Y;
        public int Z;

        public static readonly Vec3i Zero = default;
        public static readonly Vec3i One = new (1, 1, 1);
        public static readonly Vec3i NegOne = new (-1, -1, -1);
        public static readonly Vec3i UnitX = new (1, 0, 0);
        public static readonly Vec3i UnitNegX = new (-1, 0, 0);
        public static readonly Vec3i UnitY = new (0, 1, 0);
        public static readonly Vec3i UnitNegY = new (0, -1, 0);
        public static readonly Vec3i UnitZ = new (0, 0, 1);
        public static readonly Vec3i UnitNegZ = new (0, 0, -1);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Vec3i (int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString () {
            return string.Format (System.Globalization.CultureInfo.InvariantCulture, "({0}, {1}, {2})", X, Y, Z);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Vec3iExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i Neg (this Vec3i lhs) {
            return new (-lhs.X, -lhs.Y, -lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i Add (this Vec3i lhs, Vec3i rhs) {
            return new (lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i Sub (this Vec3i lhs, Vec3i rhs) {
            return new (lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i Mul (this Vec3i lhs, Vec3i rhs) {
            return new (lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i Scale (this Vec3i lhs, int rhs) {
            return new (lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Vec3i lhs, Vec3i rhs) {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToVec3f (this Vec3i lhs) {
            return new Vec3f (lhs.X, lhs.Y, lhs.Z);
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Vector3Int ToVector2Int (this Vec3i lhs) {
            return new UnityEngine.Vector3Int (lhs.X, lhs.Y, lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i ToVec3i (this UnityEngine.Vector3Int lhs) {
            return new (lhs.x, lhs.y, lhs.z);
        }
#endif
    }
}
