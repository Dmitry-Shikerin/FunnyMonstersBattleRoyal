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
    public sealed class Rng {
        uint _seed;
        uint _step;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Rng (uint seed) {
            _seed = seed;
            _step = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Rng SetSeed (uint seed) {
            _seed = seed;
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Rng SetStep (uint step) {
            _step = step;
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public uint Seed () {
            return _seed;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public uint Step () {
            return _step;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int IntRange (int start, int end) {
            return HashIntRange (start, end, _seed, _step++);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public float FloatRange (float start, float end) {
            return HashFloatRange (start, end, _seed, _step++);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int HashIntRange (int start, int end, uint seed, uint step) {
#if DEBUG
            if (start > end) { throw new System.Exception ("некорректный диапазон"); }
#endif
            var v = Hash (seed, step);
            return (int) (v % (end - start + 1)) + start;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float HashFloatRange (float start, float end, uint seed, uint step) {
#if DEBUG
            if (start > end) { throw new System.Exception ("некорректный диапазон"); }
#endif
            var v = Hash (seed, step);
            return (int) (v % 10001) / 10000f * (end - start) + start;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static uint Hash (uint seed, uint step) {
            var h32 = seed + 374761393U + 4U + step * 3266489917U;
            h32 = (h32 << 17) | (h32 >> (32 - 17)) * 668265263U;
            h32 ^= h32 >> 15;
            h32 *= 2246822519U;
            h32 ^= h32 >> 13;
            h32 *= 3266489917U;
            h32 ^= h32 >> 16;
            return h32;
        }
    }
}
