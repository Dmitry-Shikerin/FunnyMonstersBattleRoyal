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
    public struct Quat {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly Quat Ident = new Quat (0f, 0f, 0f, 1f);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Quat (float x, float y, float z, float w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat Euler (in Vec3f v) {
            return Euler (v.X, v.Y, v.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat Euler (float x, float y, float z) {
            // порядок - ZXY.
            x *= 0.5f;
            y *= 0.5f;
            z *= 0.5f;
            var sX = MathFast.Sin (x);
            var sY = MathFast.Sin (y);
            var sZ = MathFast.Sin (z);
            var cX = MathFast.Cos (x);
            var cY = MathFast.Cos (y);
            var cZ = MathFast.Cos (z);

            return new (
                sX * cY * cZ + sY * sZ * cX,
                sY * cX * cZ - sX * sZ * cY,
                sZ * cX * cY - sX * sY * cZ,
                cX * cY * cZ + sY * sZ * sX
            );
        }

        public override string ToString () {
            return string.Format (System.Globalization.CultureInfo.InvariantCulture, "({0:F5}, {1:F5}, {2:F5}, {3:F5})", X, Y, Z, W);
        }
    }

#if ENABLE_IL2CPP
        [Il2CppSetOption (Option.NullChecks, false)]
        [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class QuatExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat Norm (this Quat lhs) {
            var invLen = 1f / (float) Math.Sqrt (lhs.X * lhs.X + lhs.Y * lhs.Y + lhs.Z * lhs.Z + lhs.W * lhs.W);
            return new (lhs.X * invLen, lhs.Y * invLen, lhs.Z * invLen, lhs.W * invLen);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutNorm (this ref Quat lhs) {
            var invLen = 1f / (float) Math.Sqrt (lhs.X * lhs.X + lhs.Y * lhs.Y + lhs.Z * lhs.Z + lhs.W * lhs.W);
            lhs.X *= invLen;
            lhs.Y *= invLen;
            lhs.Z *= invLen;
            lhs.W *= invLen;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat Conjugate (this Quat lhs) {
            return new (-lhs.X, -lhs.Y, -lhs.Z, lhs.W);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat LerpTo (this Quat lhs, Quat rhs, float t) {
            if (t > 1f) {
                return rhs;
            }
            if (t < 0f) {
                return lhs;
            }
            return new Quat (
                (rhs.X - lhs.X) * t + lhs.X,
                (rhs.Y - lhs.Y) * t + lhs.Y,
                (rhs.Z - lhs.Z) * t + lhs.Z,
                (rhs.W - lhs.W) * t + lhs.W).Norm ();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat Mul (this Quat lhs, Quat rhs) {
            return new (
                lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.W * rhs.Y - lhs.X * rhs.Z + lhs.Y * rhs.W + lhs.Z * rhs.X,
                lhs.W * rhs.Z + lhs.X * rhs.Y - lhs.Y * rhs.X + lhs.Z * rhs.W,
                lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static void MutMul (this ref Quat lhs, Quat rhs) {
            var x = lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y;
            var y = lhs.W * rhs.Y - lhs.X * rhs.Z + lhs.Y * rhs.W + lhs.Z * rhs.X;
            var z = lhs.W * rhs.Z + lhs.X * rhs.Y - lhs.Y * rhs.X + lhs.Z * rhs.W;
            lhs.W = lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z;
            lhs.X = x;
            lhs.Y = y;
            lhs.Z = z;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f MulPoint (this Quat lhs, Vec3f rhs) {
            var qX = 2f * (lhs.Y * rhs.Z - lhs.Z * rhs.Y);
            var qY = 2f * (lhs.Z * rhs.X - lhs.X * rhs.Z);
            var qZ = 2f * (lhs.X * rhs.Y - lhs.Y * rhs.X);

            return new (
                rhs.X + lhs.W * qX + lhs.Y * qZ - lhs.Z * qY,
                rhs.Y + lhs.W * qY + lhs.Z * qX - lhs.X * qZ,
                rhs.Z + lhs.W * qZ + lhs.X * qY - lhs.Y * qX);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Quat lhs, Quat rhs) {
            return
                (lhs.X - rhs.X) * (lhs.X - rhs.X)
                + (lhs.Y - rhs.Y) * (lhs.Y - rhs.Y)
                + (lhs.Z - rhs.Z) * (lhs.Z - rhs.Z)
                + (lhs.W - rhs.W) * (lhs.W - rhs.W) < MathFast.Epsilon;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f ToEuler (this Quat q) {
            var xw2 = q.X * q.W * 2f;
            var yw2 = q.Y * q.W * 2f;
            var zw2 = q.Z * q.W * 2f;
            var xy2 = q.X * q.Y * 2f;
            var yz2 = q.Y * q.Z * 2f;
            var zx2 = q.Z * q.X * 2f;
            var xx = q.X * q.X;
            var yy = q.Y * q.Y;
            var zz = q.Z * q.Z;
            var ww = q.W * q.W;
            var y1 = yz2 - xw2;

            const float Threshold = (1f - 2f * MathFast.Epsilon) * (1f - 2f * MathFast.Epsilon);

            if (y1 * y1 < Threshold) {
                return new Vec3f (
                    -(float) Math.Asin (y1),
                    (float) Math.Atan2 (zx2 + yw2, zz + ww - xx - yy),
                    (float) Math.Atan2 (xy2 + zw2, yy + ww - xx - zz));
            }
            return new Vec3f (
                (float) -Math.Asin (MathFast.Clamp (y1, -1f, 1f)),
                0f,
                (float) Math.Atan2 (2f * (zx2 * xw2 + yw2 * yz2), -zx2 * zx2 + yw2 * yw2 - yz2 * yz2 + xw2 * xw2));
        }

#if UNITY_2021_3_OR_NEWER
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion ToQuaternion (this Quat v) {
            return new (v.X, v.Y, v.Z, v.W);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Quat ToQuat (this UnityEngine.Quaternion v) {
            return new (v.x, v.y, v.z, v.w);
        }
#endif
    }
}
