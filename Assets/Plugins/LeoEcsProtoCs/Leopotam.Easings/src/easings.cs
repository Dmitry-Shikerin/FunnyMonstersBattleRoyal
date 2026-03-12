// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Easings {
    public enum Ease {
        Linear,
        QuadIn,
        QuadOut,
        QuadInOut,
        CubicIn,
        CubicOut,
        CubicInOut,
        QuartIn,
        QuartOut,
        QuartInOut,
        QuintIn,
        QuintOut,
        QuintInOut,
        SineIn,
        SineOut,
        SineInOut,
        BounceIn,
        BounceOut,
        BounceInOut,
        BackIn,
        BackOut,
        BackInOut,
        ElasticIn,
        ElasticOut,
        ElasticInOut,
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class EaseExtensions {
        static readonly Func<float, float>[] _easeHandlers = {
            EaseLinear,
            EaseQuadIn,
            EaseQuadOut,
            EaseQuadInOut,
            EaseCubicIn,
            EaseCubicOut,
            EaseCubicInOut,
            EaseQuartIn,
            EaseQuartOut,
            EaseQuartInOut,
            EaseQuintIn,
            EaseQuintOut,
            EaseQuintInOut,
            EaseSineIn,
            EaseSineOut,
            EaseSineInOut,
            EaseBounceIn,
            EaseBounceOut,
            EaseBounceInOut,
            EaseBackIn,
            EaseBackOut,
            EaseBackInOut,
            EaseElasticIn,
            EaseElasticOut,
            EaseElasticInOut
        };

        const float Pi = (float) Math.PI;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Raw (this Ease ease, float value) {
            return _easeHandlers[(int) ease] (value);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Clamp (this Ease ease, float value) {
            if (value < 0f) {
                value = 0f;
            } else {
                if (value > 1f) {
                    value = 1f;
                }
            }
            return _easeHandlers[(int) ease] (value);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Repeat (this Ease ease, float value) {
            value -= value >= 0f ? (int) value : (int) value - 1;
            return _easeHandlers[(int) ease] (value);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float PingPong (this Ease ease, float value) {
            value -= (value >= 0f ? (int) (value * 0.5f) : (int) (value * 0.5f) - 1) * 2f + 1f;
            if (value < 0f) { value = -value; }
            return _easeHandlers[(int) ease] (1f - value);
        }

        static float EaseLinear (float value) {
            return value;
        }

        static float EaseQuadIn (float value) {
            return value * value;
        }

        static float EaseQuadOut (float value) {
            return -value * (value - 2f);
        }

        static float EaseQuadInOut (float value) {
            value *= 2f;
            if (value < 1f) { return 0.5f * value * value; }
            value--;
            return -0.5f * (value * (value - 2f) - 1f);
        }

        static float EaseCubicIn (float value) {
            return value * value * value;
        }

        static float EaseCubicOut (float value) {
            value--;
            return value * value * value + 1f;
        }

        static float EaseCubicInOut (float value) {
            value *= 2f;
            if (value < 1f) { return 0.5f * value * value * value; }
            value -= 2f;
            return 0.5f * (value * value * value + 2f);
        }

        static float EaseQuartIn (float value) {
            var vv = value * value;
            return vv * vv;
        }

        static float EaseQuartOut (float value) {
            value--;
            var vv = value * value;
            return -(vv * vv - 1f);
        }

        static float EaseQuartInOut (float value) {
            value *= 2f;
            if (value < 1f) {
                var vv1 = value * value;
                return 0.5f * vv1 * vv1;
            }
            value -= 2f;
            var vv2 = value * value;
            return -0.5f * (vv2 * vv2 - 2);
        }

        static float EaseQuintIn (float value) {
            var vv = value * value;
            return vv * vv * value;
        }

        static float EaseQuintOut (float value) {
            value--;
            var vv = value * value;
            return vv * vv * value + 1;
        }

        static float EaseQuintInOut (float value) {
            value *= 2f;
            if (value < 1f) {
                var vv1 = value * value;
                return 0.5f * vv1 * vv1 * value;
            }
            value -= 2f;
            var vv2 = value * value;
            return 0.5f * (vv2 * vv2 * value + 2);
        }

        static float EaseSineIn (float value) {
            return 1f - (float) Math.Cos (value * (Pi * 0.5f));
        }

        static float EaseSineOut (float value) {
            return (float) Math.Sin (value * (Pi * 0.5f));
        }

        static float EaseSineInOut (float value) {
            return -0.5f * ((float) Math.Cos (Pi * value) - 1f);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        static float EaseBounceIn (float value) {
            return 1f - EaseBounceOut (1f - value);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        static float EaseBounceOut (float value) {
            if (value < 1f / 2.75f) {
                return 7.5625f * value * value;
            }

            if (value < 2f / 2.75f) {
                value -= 1.5f / 2.75f;
                return 7.5625f * value * value + 0.75f;
            }
            if (value < 2.5f / 2.75f) {
                value -= 2.25f / 2.75f;
                return 7.5625f * value * value + 0.9375f;
            }
            value -= 2.625f / 2.75f;
            return 7.5625f * value * value + 0.984375f;
        }

        static float EaseBounceInOut (float value) {
            return value < 0.5f
                ? EaseBounceIn (value * 2) * 0.5f
                : EaseBounceOut (value * 2 - 1f) * 0.5f + 0.5f;
        }

        static float EaseBackIn (float value) {
            return value * value * ((1.70158f + 1f) * value - 1.70158f);
        }

        static float EaseBackOut (float value) {
            value -= 1f;
            return 1 + ((1.70158f + 1) * value + 1.70158f) * value * value;
        }

        static float EaseBackInOut (float value) {
            var s = 1.70158f * 1.525f;
            value *= 2f;
            if (value < 1f) {
                return 0.5f * (value * value * ((s + 1f) * value - s));
            }
            value -= 2f;
            return 0.5f * (value * value * ((s + 1f) * value + s) + 2f);
        }

        static float EaseElasticIn (float value) {
            if (value <= float.Epsilon) { return 0f; }
            if (value >= 1f) { return 1f; }
            return -(float) Math.Pow (2, 10 * value - 10) * (float) Math.Sin ((value * 10f - 10.75f) * Pi * 2f / 3f);
        }

        static float EaseElasticOut (float value) {
            if (value <= float.Epsilon) { return 0f; }
            if (value >= 1f) { return 1f; }

            return (float) Math.Pow (2, -10 * value) * (float) Math.Sin ((value * 10f - 0.75f) * Pi * 2f / 3f) + 1f;
        }

        static float EaseElasticInOut (float value) {
            if (value <= float.Epsilon) { return 0f; }
            if (value >= 1f) { return 1f; }
            return value < 0.5f
                ? -((float) Math.Pow (2, 20 * value - 10) * (float) Math.Sin ((20 * value - 11.125) * Pi * 2f / 4.5f)) * 0.5f
                : (float) Math.Pow (2, -20 * value + 10) * (float) Math.Sin ((20 * value - 11.125) * Pi * 2f / 4.5f) * 0.5f + 1;
        }
    }
}

#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices {
    enum Option {
        NullChecks = 1,
        ArrayBoundsChecks = 2
    }

    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute (Option option, object value) { Option = option; Value = value; }
    }
}
#endif
