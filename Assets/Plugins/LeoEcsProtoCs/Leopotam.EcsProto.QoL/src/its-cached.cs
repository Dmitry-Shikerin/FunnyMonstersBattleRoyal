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
    public delegate int SortCachingCallback (ProtoEntity a, ProtoEntity b);

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    sealed class ProtoEntityComparer : IComparer<ProtoEntity> {
        public SortCachingCallback Handler;
        public int Compare (ProtoEntity x, ProtoEntity y) => Handler (y, x);
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ProtoItCached : IProtoIt {
        readonly ProtoIt _it;
        readonly ProtoEntityComparer _comparer;
        bool _cached;
        ProtoEntity[] _cachedEntities;
        int _cachedIdx;
        int _cachedLen;
        ProtoEntity _cachedEntity;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoItCached (Type[] iTypes) {
            _comparer = new ();
            _it = new (iTypes);
            _cachedEntities = Array.Empty<ProtoEntity> ();
            _cached = false;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoIt Init (ProtoWorld world) {
            _it.Init (world);
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void BeginCaching () {
#if DEBUG
            if (_cached) { throw new Exception ("итератор уже закеширован, необходимо сначала сбросить старый кеш"); }
#endif
            var cap = _it.World ().EntityGens ().Cap ();
            if (_cachedEntities.Length != cap) {
                _cachedEntities = new ProtoEntity[cap];
            }
            _cachedLen = 0;
            _it.Begin ();
            while (_it.Next ()) {
                _cachedEntities[_cachedLen++] = _it.Entity ();
            }
#if DEBUG
            _it.AddBlocker (2);
#endif
            _cached = true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void EndCaching () {
#if DEBUG
            if (!_cached) { throw new Exception ("итератор не был закеширован"); }
            _it.AddBlocker (-2);
#endif
            _cached = false;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Sort (SortCachingCallback sortCb) {
#if DEBUG
            if (sortCb == null) { throw new Exception ("для сортировки необходимо указать обработчик"); }
            if (!_cached) { throw new Exception ("для сортировки итератор должен быть закеширован"); }
#endif
            if (_cachedLen > 0) {
                _comparer.Handler = sortCb;
                Array.Sort (_cachedEntities, 0, _cachedLen, _comparer);
                _comparer.Handler = null;
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsCached () => _cached;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (ProtoEntity[] entities, int len) CachedData () => (_cachedEntities, _cachedLen);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ItEnumerator GetEnumerator () => new (this);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool[] Includes () => _it.Includes ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoWorld World () => _it.World ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (ProtoEntity entity) => _it.Has (entity);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Begin () {
            if (!_cached) {
                _it.Begin ();
            } else {
                _cachedIdx = _cachedLen;
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Next () {
            if (!_cached) {
                return _it.Next ();
            }
            if (_cachedIdx == 0) {
                End ();
                return false;
            }
            _cachedIdx--;
            _cachedEntity = _cachedEntities[_cachedIdx];
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void End () {
            if (_cached) {
                _cachedIdx = 0;
            } else {
                _it.End ();
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Entity () => _cached ? _cachedEntity : _it.Entity ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool MinPool () => _it.MinPool ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> IncMask () => _it.IncMask ();

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public readonly ref struct ItEnumerator {
            readonly ProtoItCached _it;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public ItEnumerator (ProtoItCached it) {
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

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ProtoItExcCached : IProtoIt {
        readonly ProtoEntityComparer _comparer;
        ProtoItExc _it;
        bool _cached;
        ProtoEntity[] _cachedEntities;
        int _cachedIdx;
        int _cachedLen;
        ProtoEntity _cachedEntity;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoItExcCached (Type[] iTypes, Type[] eTypes) {
            _comparer = new ();
            _it = new (iTypes, eTypes);
            _cachedEntities = Array.Empty<ProtoEntity> ();
            _cached = false;
        }

        public IProtoIt Init (ProtoWorld world) {
            _it.Init (world);
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void BeginCaching () {
#if DEBUG
            if (_cached) { throw new Exception ("итератор уже закеширован, необходимо сначала сбросить старый кеш"); }
#endif
            var cap = _it.World ().EntityGens ().Cap ();
            if (_cachedEntities.Length != cap) {
                _cachedEntities = new ProtoEntity[cap];
            }
            _cachedLen = 0;
            _it.Begin ();
            while (_it.Next ()) {
                _cachedEntities[_cachedLen++] = _it.Entity ();
            }
#if DEBUG
            _it.AddBlocker (2);
#endif
            _cached = true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void EndCaching () {
#if DEBUG
            if (!_cached) { throw new Exception ("итератор не был закеширован"); }
            _it.AddBlocker (-2);
#endif
            _cached = false;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Sort (SortCachingCallback sortCb) {
#if DEBUG
            if (sortCb == null) { throw new Exception ("для сортировки необходимо указать обработчик"); }
            if (!_cached) { throw new Exception ("для сортировки итератор должен быть закеширован"); }
#endif
            if (_cachedLen > 0) {
                _comparer.Handler = sortCb;
                Array.Sort (_cachedEntities, 0, _cachedLen, _comparer);
                _comparer.Handler = null;
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsCached () => _cached;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (ProtoEntity[] entities, int len) CachedData () => (_cachedEntities, _cachedLen);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ItEnumerator GetEnumerator () => new (this);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool[] Includes () => _it.Includes ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool[] Excludes () => _it.Excludes ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoWorld World () => _it.World ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (ProtoEntity entity) => _it.Has (entity);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Begin () {
            if (!_cached) {
                _it.Begin ();
            } else {
                _cachedIdx = _cachedLen;
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Next () {
            if (!_cached) {
                return _it.Next ();
            }
            if (_cachedIdx == 0) {
                End ();
                return false;
            }
            _cachedIdx--;
            _cachedEntity = _cachedEntities[_cachedIdx];
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void End () {
            if (_cached) {
                _cachedIdx = 0;
            } else {
                _it.End ();
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Entity () => _cached ? _cachedEntity : _it.Entity ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool MinPool () => _it.MinPool ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> IncMask () => _it.IncMask ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> ExcMask () => _it.ExcMask ();

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public readonly ref struct ItEnumerator {
            readonly ProtoItExcCached _it;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public ItEnumerator (ProtoItExcCached it) {
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
