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
    public struct Vec2i {
        public int X;
        public int Y;

        public static readonly Vec2i Zero = default;
        public static readonly Vec2i One = new (1, 1);
        public static readonly Vec2i NegOne = new (-1, -1);
        public static readonly Vec2i UnitX = new (1, 0);
        public static readonly Vec2i UnitNegX = new (-1, 0);
        public static readonly Vec2i UnitY = new (0, 1);
        public static readonly Vec2i UnitNegY = new (0, -1);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Vec2i (int x, int y) {
            X = x;
            Y = y;
        }

        public override string ToString () {
            return string.Format (System.Globalization.CultureInfo.InvariantCulture, "({0}, {1})", X, Y);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Vec2iExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i Neg (this Vec2i lhs) {
            return new (-lhs.X, -lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i Add (this Vec2i lhs, Vec2i rhs) {
            return new (lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i Sub (this Vec2i lhs, Vec2i rhs) {
            return new (lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i Mul (this Vec2i lhs, Vec2i rhs) {
            return new (lhs.X * rhs.X, lhs.Y * rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i Scale (this Vec2i lhs, int rhs) {
            return new (lhs.X * rhs, lhs.Y * rhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Vec2i lhs, Vec2i rhs) {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f ToVec2f (this Vec2i lhs) {
            return new Vec2f (lhs.X, lhs.Y);
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Vector2Int ToVector2Int (this Vec2i lhs) {
            return new UnityEngine.Vector2Int (lhs.X, lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i ToVec2i (this UnityEngine.Vector2Int lhs) {
            return new (lhs.x, lhs.y);
        }
#endif
    }
}
