// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Globalization;
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
    public struct Fix32 {
        public int RawValue;

        internal const int FracBits = 16;
        internal const int FracRange = 1 << FracBits;
        internal const int FracMask = FracRange - 1;
        internal const float InvFracRange = 1f / FracRange;
        internal const int TruncMask = ~FracMask;
        internal const int HalfRaw = 1 << (FracBits - 1);

        public static readonly Fix32 Zero;
        public static readonly Fix32 One = 1.ToFix32 ();
        public static readonly Fix32 NegOne = (-1).ToFix32 ();
        public static readonly Fix32 MaxValue = new (int.MaxValue);
        public static readonly Fix32 MinValue = new (int.MinValue + 1);
        public static readonly Fix32 Pi = MathFast.Pi.ToFix32 ();
        public static readonly Fix32 PiHalf = (MathFast.Pi * 0.5f).ToFix32 ();
        public static readonly Fix32 InvPi2 = (1f / (MathFast.Pi * 2f)).ToFix32 ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Fix32 (int rawValue) {
            RawValue = rawValue;
        }

        public override string ToString () {
            return this.ToFloat ().ToString (CultureInfo.InvariantCulture);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Fixed32Extensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Neg (this Fix32 lhs) {
            return new Fix32 (-lhs.RawValue);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Abs (this Fix32 lhs) {
            var mask = lhs.RawValue >> 31;
            return new ((lhs.RawValue + mask) ^ mask);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Ceil (this Fix32 lhs) {
            return new ((lhs.RawValue + Fix32.FracMask) & Fix32.TruncMask);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Round (this Fix32 lhs) {
            return new ((lhs.RawValue + Fix32.HalfRaw) & Fix32.TruncMask);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Floor (this Fix32 lhs) {
            return new (lhs.RawValue & Fix32.TruncMask);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Sign (this Fix32 lhs) {
            return (lhs.RawValue >> 31) | (int) ((uint) -lhs.RawValue >> 31);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Min (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue < rhs.RawValue ? lhs : rhs;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Max (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue > rhs.RawValue ? lhs : rhs;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 LerpTo (this Fix32 lhs, Fix32 rhs, Fix32 t) {
            if (t.GEquals (Fix32.One)) {
                return rhs;
            }
            if (t.LEquals (Fix32.Zero)) {
                return lhs;
            }
            return rhs.Sub (lhs).Mul (t).Add (lhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 LerpToUnclamped (this Fix32 lhs, Fix32 rhs, Fix32 t) {
            return rhs.Sub (lhs).Mul (t).Add (lhs);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Mul (this Fix32 lhs, Fix32 rhs) {
            return new ((int) ((lhs.RawValue * (long) rhs.RawValue) >> Fix32.FracBits));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Div (this Fix32 lhs, Fix32 rhs) {
            return new ((int) (((long) lhs.RawValue << Fix32.FracBits) / rhs.RawValue));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Add (this Fix32 lhs, Fix32 rhs) {
            return new (lhs.RawValue + rhs.RawValue);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Sub (this Fix32 lhs, Fix32 rhs) {
            return new (lhs.RawValue - rhs.RawValue);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 Sqrt (this Fix32 lhs) {
            if (lhs.RawValue <= 0) { return Fix32.Zero; }
            var r = (uint) lhs.RawValue;
            uint b = 0x40000000;
            uint q = 0;
            while (b > 0x40) {
                var t = q + b;
                if (r >= t) {
                    r -= t;
                    q = t + b;
                }
                r <<= 1;
                b >>= 1;
            }
            q >>= 8;
            return new Fix32 ((int) q);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Greater (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue > rhs.RawValue;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool GEquals (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue >= rhs.RawValue;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Less (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue < rhs.RawValue;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool LEquals (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue <= rhs.RawValue;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo (this Fix32 lhs, Fix32 rhs) {
            return lhs.RawValue == rhs.RawValue;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 ToFix32 (this int lhs) {
#if DEBUG
            if (lhs <= -32768 || lhs >= 32768) { throw new Exception ("значение за пределами разрешенного диапазона"); }
#endif
            return new (lhs >= 0 ? lhs << Fix32.FracBits : -(-lhs << Fix32.FracBits));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Fix32 ToFix32 (this float lhs) {
#if DEBUG
            if ((int) lhs <= -32768 || (int) lhs >= 32768) { throw new Exception ("значение за пределами разрешенного диапазона"); }
#endif
            return new Fix32 ((int) (lhs * Fix32.FracRange));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float ToFloat (this Fix32 lhs) {
            return lhs.RawValue * Fix32.InvFracRange;
        }
    }
}
