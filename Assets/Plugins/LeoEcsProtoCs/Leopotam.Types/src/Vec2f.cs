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
    public struct Vec2f {
        public float X;
        public float Y;

        public static readonly Vec2f Zero = default;
        public static readonly Vec2f One = new (1f, 1f);
        public static readonly Vec2f NegOne = new (-1f, -1f);
        public static readonly Vec2f UnitX = new (1f, 0f);
        public static readonly Vec2f UnitNegX = new (-1f, 0f);
        public static readonly Vec2f UnitY = new (0f, 1f);
        public static readonly Vec2f UnitNegY = new (0f, -1f);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Vec2f (float x, float y) {
            X = x;
            Y = y;
        }

        public override string ToString () {
            return string.Format (System.Globalization.CultureInfo.InvariantCulture, "({0:F5}, {1:F5})", X, Y);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Vec2fExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Neg (this Vec2f lhs) {
            return new Vec2f (-lhs.X, -lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Norm (this Vec2f lhs) {
            var invSqrt = 1f / (float) Math.Sqrt (lhs.X * lhs.X + lhs.Y * lhs.Y);
            return new Vec2f (lhs.X * invSqrt, lhs.Y * invSqrt);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Dot (this Vec2f lhs, Vec2f rhs) {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f DirTo (this Vec2f lhs, Vec2f rhs) {
            var x = rhs.X - lhs.X;
            var y = rhs.Y - lhs.Y;
            var invSqrt = 1f / (float) Math.Sqrt (x * x + y * y);
            return new (x * invSqrt, y * invSqrt);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float AngleBetweenDirs (this Vec2f lhs, Vec2f rhs) {
            return (float) Math.Acos (lhs.X * rhs.X + lhs.Y * rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float AngleBetweenDirsSigned (this Vec2f lhs, Vec2f rhs) {
            return (float) Math.Acos (lhs.X * rhs.X + lhs.Y * rhs.Y) *
                   (lhs.X * rhs.Y - lhs.Y * rhs.X > 0f ? 1f : -1f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f DirFromAngle (this float lhs) {
            return new ((float) Math.Cos (lhs), (float) Math.Sin (lhs));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Reflect (this Vec2f direction, Vec2f normal) {
            var dist = -2f * (normal.X * direction.X + normal.Y * direction.Y);
            return new (normal.X * dist + direction.X, normal.Y * dist + direction.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Min (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X < rhs.X ? lhs.X : rhs.X, lhs.Y < rhs.Y ? lhs.Y : rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Max (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X > rhs.X ? lhs.X : rhs.X, lhs.Y > rhs.Y ? lhs.Y : rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f LerpTo (this Vec2f lhs, Vec2f rhs, float t) {
            if (t > 1f) {
                return rhs;
            }
            if (t < 0f) {
                return lhs;
            }
            return new ((rhs.X - lhs.X) * t + lhs.X, (rhs.Y - lhs.Y) * t + lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f LerpToUnclamped (this Vec2f lhs, Vec2f rhs, float t) {
            return new ((rhs.X - lhs.X) * t + lhs.X, (rhs.Y - lhs.Y) * t + lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float LenSqr (this Vec2f v) {
            return v.X * v.X + v.Y * v.Y;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Len (this Vec2f v) {
            return (float) Math.Sqrt (v.X * v.X + v.Y * v.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Dist (this Vec2f lhs, Vec2f rhs) {
            var x = lhs.X - rhs.X;
            var y = lhs.Y - rhs.Y;
            return (float) Math.Sqrt (x * x + y * y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float DistSqr (this Vec2f lhs, Vec2f rhs) {
            var x = lhs.X - rhs.X;
            var y = lhs.Y - rhs.Y;
            return x * x + y * y;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Scale (this Vec2f lhs, float rhs) {
            return new (lhs.X * rhs, lhs.Y * rhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Mul (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X * rhs.X, lhs.Y * rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Div (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X / rhs.X, lhs.Y / rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f DivScale (this Vec2f lhs, float rhs) {
            var invRhs = 1f / rhs;
            return new (lhs.X * invRhs, lhs.Y * invRhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Add (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f Sub (this Vec2f lhs, Vec2f rhs) {
            return new (lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Vec2f lhs, Vec2f rhs) {
            return (lhs.X - rhs.X) * (lhs.X - rhs.X) + (lhs.Y - rhs.Y) * (lhs.Y - rhs.Y) < MathFast.Epsilon;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToXY0 (this Vec2f lhs) {
            return new (lhs.X, lhs.Y, 0f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToX0Y (this Vec2f lhs) {
            return new (lhs.X, 0f, lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2i ToVec2i (this Vec2f lhs, bool rounded) {
            return rounded
                ? new Vec2i (MathFast.Round (lhs.X), MathFast.Round (lhs.Y))
                : new Vec2i (MathFast.Floor (lhs.X), MathFast.Floor (lhs.Y));
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f ToVec2f (this UnityEngine.Vector2 lhs) {
            return new (lhs.x, lhs.y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Vector2 ToVector2 (this Vec2f lhs) {
            return new (lhs.X, lhs.Y);
        }
#endif
    }
}
