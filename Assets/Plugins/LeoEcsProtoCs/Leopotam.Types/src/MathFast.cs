// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Types {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class MathFast {
        public const float Epsilon = 1e-4f;

        public const float Pi = (float) System.Math.PI;

        public const float Pi2 = Pi * 2f;

        public const float PiDiv2 = Pi * 0.5f;

        public const float Deg2Rad = Pi / 180f;

        public const float Rad2Deg = 1f / Deg2Rad;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Min (float a, float b) {
            return a >= b ? b : a;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Min (int a, int b) {
            return a >= b ? b : a;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Max (float a, float b) {
            return a >= b ? a : b;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Max (int a, int b) {
            return a >= b ? a : b;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Abs (float v) {
            return v >= 0f ? v : -v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Abs (int v) {
            return v >= 0 ? v : -v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Sign (float v) {
            return v > 0f ? 1 : v < 0f ? -1 : 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Sign (int v) {
            return v > 0 ? 1 : v < 0 ? -1 : 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Clamp (float data, float min, float max) {
            if (data < min) {
                return min;
            }
            if (data > max) {
                return max;
            }
            return data;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Clamp (int data, int min, int max) {
            if (data < min) {
                return min;
            }
            if (data > max) {
                return max;
            }
            return data;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Lerp (float a, float b, float t) {
            if (t <= 0f) {
                return a;
            }
            if (t >= 1f) {
                return b;
            }
            return a + (b - a) * t;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float LerpUnclamped (float a, float b, float t) {
            return a + (b - a) * t;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float LerpInv (float a, float b, float value) {
            var data = (value - a) / (b - a);
            if (data < 0f) {
                return 0f;
            }
            if (data > 1f) {
                return 1f;
            }
            return data;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool ApproxTo (this float lhs, float rhs) {
            lhs -= rhs;
            return lhs > 0f ? lhs < Epsilon : lhs > -Epsilon;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Floor (float data) {
            return data >= 0f ? (int) data : (int) data - 1;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Round (float data) {
            return data >= 0f ? (int) (data + 0.5f) : (int) (data - 0.5f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Sin (float v) {
            return (float) System.Math.Sin (v);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Cos (float v) {
            return (float) System.Math.Cos (v);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Vec3f NormEuler (in Vec3f euler) {
            var ret = euler;
            while (ret.X > Pi2) { ret.X -= Pi2; }
            while (ret.X < 0f) { ret.X += Pi2; }
            while (ret.Y > Pi2) { ret.Y -= Pi2; }
            while (ret.Y < 0f) { ret.Y += Pi2; }
            while (ret.Z > Pi2) { ret.Z -= Pi2; }
            while (ret.Z < 0f) { ret.Z += Pi2; }
            return ret;
        }
    }
}
