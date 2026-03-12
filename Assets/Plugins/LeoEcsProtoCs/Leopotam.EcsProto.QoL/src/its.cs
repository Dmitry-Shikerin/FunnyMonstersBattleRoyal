// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.QoL {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class It {
        public static Type[] List (params object[] components) {
            var types = new Type[components.Length];
            for (var i = 0; i < components.Length; i++) {
                types[i] = components[i].GetType ();
            }
            return types;
        }

#if DEBUG
        [Obsolete ("следует использовать It.List()")]
#endif
        public static Type[] Inc (params object[] components) => List (components);
#if DEBUG
        [Obsolete ("следует использовать It.List()")]
#endif
        public static Type[] Exc (params object[] components) => List (components);

        public static Type[] Inc<T1> () => new[] { typeof (T1) };
        public static Type[] Inc<T1, T2> () => new[] { typeof (T1), typeof (T2) };
        public static Type[] Inc<T1, T2, T3> () => new[] { typeof (T1), typeof (T2), typeof (T3) };

        public static Type[] Inc<T1, T2, T3, T4> () => new[] {
            typeof (T1), typeof (T2), typeof (T3), typeof (T4)
        };

        public static Type[] Inc<T1, T2, T3, T4, T5> () => new[] {
            typeof (T1), typeof (T2), typeof (T3), typeof (T4), typeof (T5)
        };

        public static Type[] Inc<T1, T2, T3, T4, T5, T6> () => new[] {
            typeof (T1), typeof (T2), typeof (T3), typeof (T4), typeof (T5), typeof (T6)
        };

        public static Type[] Exc<T1> () => new[] { typeof (T1) };
        public static Type[] Exc<T1, T2> () => new[] { typeof (T1), typeof (T2) };
        public static Type[] Exc<T1, T2, T3> () => new[] { typeof (T1), typeof (T2), typeof (T3) };

        public static ProtoItChain Chain<T> () where T : struct {
            return new ProtoItChain ().Inc<T> ();
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ProtoItChain {
        readonly List<Type> _iTypes;

        internal ProtoItChain () { _iTypes = new (4); }

        public ProtoItChain Inc<T> () where T : struct {
            _iTypes.Add (typeof (T));
            return this;
        }

        public ProtoItChainExc Exc<T> () where T : struct {
            return new ProtoItChainExc (_iTypes).Exc<T> ();
        }

        public ProtoIt End () {
            return new ProtoIt (_iTypes.ToArray ());
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public sealed class ProtoItChainExc {
            readonly List<Type> _iTypes;
            readonly List<Type> _eTypes;

            internal ProtoItChainExc (List<Type> iTypes) {
                _iTypes = iTypes;
                _eTypes = new (4);
            }

            public ProtoItChainExc Inc<T> () where T : struct {
                _iTypes.Add (typeof (T));
                return this;
            }

            public ProtoItChainExc Exc<T> () where T : struct {
                _eTypes.Add (typeof (T));
                return this;
            }

            public ProtoItExc End () {
                return new ProtoItExc (_iTypes.ToArray (), _eTypes.ToArray ());
            }
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class ProtoItExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Len (this IProtoIt it) {
            var len = 0;
            for (it.Begin (); it.Next ();) {
                len++;
            }
            return len;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty (this IProtoIt it) {
            for (it.Begin (); it.Next ();) {
                it.End ();
                return false;
            }
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static (ProtoEntity Entity, bool Ok) First (this IProtoIt it) {
            for (it.Begin (); it.Next ();) {
                var e = it.Entity ();
                it.End ();
                return (e, true);
            }
            return (default, false);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ItEnumerator GetEnumerator (this ProtoIt it) {
            return new ItEnumerator (it);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ItEnumeratorExc GetEnumerator (this ProtoItExc it) {
            return new ItEnumeratorExc (it);
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public readonly ref struct ItEnumerator {
            readonly ProtoIt _it;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public ItEnumerator (ProtoIt it) {
                _it = it;
                _it.Begin ();
            }

            public ProtoEntity Current {
                [MethodImpl (MethodImplOptions.AggressiveInlining)]
                get => _it.Entity ();
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public bool MoveNext () => _it.Next ();

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public void Dispose () => _it.End ();
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public readonly ref struct ItEnumeratorExc {
            readonly ProtoItExc _it;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public ItEnumeratorExc (ProtoItExc it) {
                _it = it;
                _it.Begin ();
            }

            public ProtoEntity Current {
                [MethodImpl (MethodImplOptions.AggressiveInlining)]
                get => _it.Entity ();
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public bool MoveNext () => _it.Next ();

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public void Dispose () => _it.End ();
        }
    }
}
