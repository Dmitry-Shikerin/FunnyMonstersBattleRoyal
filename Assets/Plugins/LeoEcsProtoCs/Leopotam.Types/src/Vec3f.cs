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
    public struct Vec3f {
        public float X;
        public float Y;
        public float Z;

        public static readonly Vec3f Zero = default;
        public static readonly Vec3f One = new (1f, 1f, 1f);
        public static readonly Vec3f NegOne = new (-1f, -1f, -1f);
        public static readonly Vec3f UnitX = new (1f, 0f, 0f);
        public static readonly Vec3f UnitNegX = new (-1f, 0f, 0f);
        public static readonly Vec3f UnitY = new (0f, 1f, 0f);
        public static readonly Vec3f UnitNegY = new (0f, -1f, 0f);
        public static readonly Vec3f UnitZ = new (0f, 0f, 1f);
        public static readonly Vec3f UnitNegZ = new (0f, 0f, -1f);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Vec3f (float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString () {
            return string.Format (System.Globalization.CultureInfo.InvariantCulture, "({0:F5}, {1:F5}, {2:F5})", X, Y, Z);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Vec3fExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Neg (this Vec3f lhs) {
            return new Vec3f (-lhs.X, -lhs.Y, -lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Norm (this Vec3f lhs) {
            var invSqrt = 1f / (float) Math.Sqrt (lhs.X * lhs.X + lhs.Y * lhs.Y + lhs.Z * lhs.Z);
            return new Vec3f (lhs.X * invSqrt, lhs.Y * invSqrt, lhs.Z * invSqrt);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Dot (this Vec3f lhs, Vec3f rhs) {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Cross (this Vec3f lhs, Vec3f rhs) {
            return new (
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f DirTo (this Vec3f lhs, Vec3f rhs) {
            var x = rhs.X - lhs.X;
            var y = rhs.Y - lhs.Y;
            var z = rhs.Z - lhs.Z;
            var invSqrt = 1f / (float) Math.Sqrt (x * x + y * y + z * z);
            return new (x * invSqrt, y * invSqrt, z * invSqrt);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float AngleBetweenDirs (this Vec3f lhs, Vec3f rhs) {
            return (float) Math.Acos (lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Reflect (this Vec3f direction, Vec3f normal) {
            var dist = -2f * (normal.X * direction.X + normal.Y * direction.Y + normal.Z * direction.Z);
            return new (
                normal.X * dist + direction.X,
                normal.Y * dist + direction.Y,
                normal.Z * dist + direction.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Min (this Vec3f lhs, Vec3f rhs) {
            return new (
                lhs.X < rhs.X ? lhs.X : rhs.X,
                lhs.Y < rhs.Y ? lhs.Y : rhs.Y,
                lhs.Z < rhs.Z ? lhs.Z : rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Max (this Vec3f lhs, Vec3f rhs) {
            return new (
                lhs.X > rhs.X ? lhs.X : rhs.X,
                lhs.Y > rhs.Y ? lhs.Y : rhs.Y,
                lhs.Z > rhs.Z ? lhs.Z : rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f LerpTo (this Vec3f lhs, Vec3f rhs, float t) {
            if (t > 1f) {
                return rhs;
            }
            if (t < 0f) {
                return lhs;
            }
            return new (
                (rhs.X - lhs.X) * t + lhs.X,
                (rhs.Y - lhs.Y) * t + lhs.Y,
                (rhs.Z - lhs.Z) * t + lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f LerpToUnclamped (this Vec3f lhs, Vec3f rhs, float t) {
            return new (
                (rhs.X - lhs.X) * t + lhs.X,
                (rhs.Y - lhs.Y) * t + lhs.Y,
                (rhs.Z - lhs.Z) * t + lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float LenSqr (this Vec3f v) {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Len (this Vec3f v) {
            return (float) Math.Sqrt (v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Dist (this Vec3f lhs, Vec3f rhs) {
            var x = lhs.X - rhs.X;
            var y = lhs.Y - rhs.Y;
            var z = lhs.Z - rhs.Z;
            return (float) Math.Sqrt (x * x + y * y + z * z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float DistSqr (this Vec3f lhs, Vec3f rhs) {
            var x = lhs.X - rhs.X;
            var y = lhs.Y - rhs.Y;
            var z = lhs.Z - rhs.Z;
            return x * x + y * y + z * z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Scale (this Vec3f lhs, float rhs) {
            return new (lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Mul (this Vec3f lhs, Vec3f rhs) {
            return new (lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Div (this Vec3f lhs, Vec3f rhs) {
            return new (lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f DivScale (this Vec3f lhs, float rhs) {
            var invRhs = 1f / rhs;
            return new (lhs.X * invRhs, lhs.Y * invRhs, lhs.Z * invRhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Add (this Vec3f lhs, Vec3f rhs) {
            return new (lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f Sub (this Vec3f lhs, Vec3f rhs) {
            return new (lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Vec3f lhs, Vec3f rhs) {
            return
                (lhs.X - rhs.X) * (lhs.X - rhs.X)
                + (lhs.Y - rhs.Y) * (lhs.Y - rhs.Y)
                + (lhs.Z - rhs.Z) * (lhs.Z - rhs.Z) < MathFast.Epsilon;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToXZY (this Vec3f lhs) {
            return new (lhs.X, lhs.Z, lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f ToXY (this Vec3f lhs) {
            return new (lhs.X, lhs.Y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec2f ToXZ (this Vec3f lhs) {
            return new (lhs.X, lhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3i ToVec3i (this Vec3f lhs, bool rounded) {
            return rounded
                ? new Vec3i (MathFast.Round (lhs.X), MathFast.Round (lhs.Y), MathFast.Round (lhs.Z))
                : new Vec3i (MathFast.Floor (lhs.X), MathFast.Floor (lhs.Y), MathFast.Floor (lhs.Z));
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToVec3f (this UnityEngine.Vector3 lhs) {
            return new (lhs.x, lhs.y, lhs.z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Vector3 ToVector3 (this Vec3f lhs) {
            return new (lhs.X, lhs.Y, lhs.Z);
        }
#endif
    }
}
