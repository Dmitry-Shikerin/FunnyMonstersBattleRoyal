// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto {
    public interface IProtoIt {
        IProtoIt Init (ProtoWorld world);
        ProtoWorld World ();
        bool Has (ProtoEntity entity);
        void Begin ();
        bool Next ();
        void End ();
        ProtoEntity Entity ();
    }
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ProtoIt : IProtoIt {
        ProtoWorld _world;
        Type[] _iTypes;
        IProtoPool[] _incPools;
        Slice<ulong> _incMask;
        ProtoEntity[] _entities;
        int _id;
        ProtoEntity _currEntity;
        IProtoPool _lastMinPool;
#if DEBUG
        bool _blocked;
        bool _inited;
#endif

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoIt (Type[] iTypes) {
#if DEBUG
            if (iTypes == null || iTypes.Length < 1) { throw new Exception ("некорректный список include-пулов для инициализации итератора"); }
#endif
            _iTypes = iTypes;
        }

        public IProtoIt Init (ProtoWorld world) {
            _world = world;
            var maskLen = world.EntityMaskItemLen ();
            _incMask = new (maskLen, true);
            _incPools = new IProtoPool[_iTypes.Length];
            ProtoEntity maskE = default;
            for (var i = 0; i < _iTypes.Length; i++) {
                var pool = _world.Pool (_iTypes[i]);
                EntityMask.Set (_incMask, maskLen, maskE, pool.Id ());
                _incPools[i] = pool;
            }
#if DEBUG
            _blocked = false;
            _inited = true;
#endif
            return this;
        }

        public IProtoPool[] Includes () => _incPools;

        public ProtoWorld World () => _world;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (ProtoEntity entity) {
            return _world.EntityGens ().Get (entity._id) > 0 && _world.EntityCompatibleWith (entity, _incMask);
        }

#if DEBUG
        public void AddBlocker (int amount) {
            for (var i = 0; i < _incPools.Length; i++) {
                _incPools[i].AddBlocker (amount);
            }
        }
#endif

        public void Begin () {
#if DEBUG
            if (!_inited) { throw new Exception ("итератор не инициализирован"); }
            if (_entities != null || _blocked) { throw new Exception ("итератор не был корректно закрыт в прошлый раз"); }
            // блокировка пула для проверки на множественный доступ.
            AddBlocker (1);
            _blocked = true;
#endif
            var minPool = _incPools[0];
            var minVal = minPool.Len ();
            for (ushort i = 1; i < _incPools.Length; i++) {
                var p = _incPools[i];
                var v = p.Len ();
                if (v < minVal) {
                    minVal = v;
                    minPool = p;
                }
            }
            _entities = minPool.Entities ();
            _id = minVal;
            _lastMinPool = minPool;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Next () {
#if DEBUG
            if (_entities == null) {
                throw new Exception ("итератор или еще не открыт, или уже закрыт");

            }
#endif
            while (true) {
                if (_id == 0) {
                    End ();
                    return false;
                }
                _id--;
                _currEntity = _entities[_id];
                if (_world.EntityCompatibleWith (_currEntity, _incMask)) {
                    return true;
                }
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void End () {
            _entities = null;
#if DEBUG
            if (_blocked) {
                // разблокировка пула для проверки на множественный доступ.
                AddBlocker (-1);
                _blocked = false;
            }
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Entity () => _currEntity;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool MinPool () => _lastMinPool;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> IncMask () => _incMask;
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ProtoItExc : IProtoIt {
        ProtoWorld _world;
        Type[] _iTypes;
        IProtoPool[] _incPools;
        Slice<ulong> _incMask;
        Type[] _eTypes;
        IProtoPool[] _excPools;
        Slice<ulong> _excMask;
        ProtoEntity[] _entities;
        int _id;
        ProtoEntity _currEntity;
        IProtoPool _lastMinPool;
#if DEBUG
        bool _blocked;
        bool _inited;
#endif
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoItExc (Type[] iTypes, Type[] eTypes) {
#if DEBUG
            if (iTypes == null || iTypes.Length < 1) { throw new Exception ("некорректный список include-пулов для инициализации итератора"); }
            if (eTypes == null || eTypes.Length < 1) { throw new Exception ("некорректный список exclude-пулов для инициализации итератора"); }
#endif
            _iTypes = iTypes;
            _eTypes = eTypes;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoIt Init (ProtoWorld world) {
            _world = world;
            var maskLen = world.EntityMaskItemLen ();
            _incMask = new (maskLen, true);
            _incPools = new IProtoPool[_iTypes.Length];
            ProtoEntity maskE = default;
            for (var i = 0; i < _iTypes.Length; i++) {
                var pool = _world.Pool (_iTypes[i]);
                EntityMask.Set (_incMask, maskLen, maskE, pool.Id ());
                _incPools[i] = pool;
            }
            _excMask = new (maskLen, true);
            _excPools = new IProtoPool[_eTypes.Length];
            for (var i = 0; i < _eTypes.Length; i++) {
                var pool = world.Pool (_eTypes[i]);
                EntityMask.Set (_excMask, maskLen, maskE, pool.Id ());
                _excPools[i] = pool;
            }
#if DEBUG
            _blocked = false;
            _inited = true;
#endif
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool[] Includes () => _incPools;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoWorld World () => _world;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool[] Excludes () => _excPools;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (ProtoEntity entity) {
            return _world.EntityGens ().Get (entity._id) > 0 && _world.EntityCompatibleWithAndWithout (entity, _incMask, _excMask);
        }

#if DEBUG
        public void AddBlocker (int amount) {
            for (var i = 0; i < _incPools.Length; i++) {
                _incPools[i].AddBlocker (amount);
            }
            for (var i = 0; i < _excPools.Length; i++) {
                _excPools[i].AddBlocker (amount);
            }
        }
#endif

        public void Begin () {
#if DEBUG
            if (!_inited) { throw new Exception ("итератор не инициализирован"); }
            if (_entities != null || _blocked) { throw new Exception ("итератор не был корректно закрыт в прошлый раз"); }
            // блокировка пула для проверки на множественный доступ.
            AddBlocker (1);
            _blocked = true;
#endif
            var minPool = _incPools[0];
            var minVal = minPool.Len ();
            for (ushort i = 1; i < _incPools.Length; i++) {
                var p = _incPools[i];
                var v = p.Len ();
                if (v < minVal) {
                    minVal = v;
                    minPool = p;
                }
            }
            _entities = minPool.Entities ();
            _id = minVal;
            _lastMinPool = minPool;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Next () {
#if DEBUG
            if (_entities == null) {
                throw new Exception ("итератор или еще не открыт, или уже закрыт");

            }
#endif
            while (true) {
                if (_id == 0) {
                    End ();
                    return false;
                }
                _id--;
                _currEntity = _entities[_id];
                if (_world.EntityCompatibleWithAndWithout (_currEntity, _incMask, _excMask)) {
                    return true;
                }
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void End () {
            _entities = null;
#if DEBUG
            if (_blocked) {
                // разблокировка пула для проверки на множественный доступ.
                AddBlocker (-1);
                _blocked = false;
            }
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Entity () => _currEntity;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public IProtoPool MinPool () => _lastMinPool;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> IncMask () => _incMask;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Slice<ulong> ExcMask () => _excMask;
    }
}
