// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Threads {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class ProtoThreadIt {
        int _id;
        int _from;
        int _before;
        ProtoWorld _world;
        ProtoEntity[] _entities;
        Slice<ulong> _incMask;
        Slice<ulong> _excMask;

        public ProtoThreadIt (int id) {
            _id = id;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int WorkerId () {
            return _id;
        }

        public ItEnumerator GetEnumerator () {
            return new ItEnumerator (this);
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        public ref struct ItEnumerator {
            readonly ProtoThreadIt _it;
            int _id;
            ProtoEntity _entity;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public ItEnumerator (ProtoThreadIt it) {
                _it = it;
                _id = _it._before;
                _entity = default;
            }

            public ProtoEntity Current {
                [MethodImpl (MethodImplOptions.AggressiveInlining)]
                get => _entity;
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public bool MoveNext () {
                while (true) {
                    if (_id == _it._from) {
                        return false;
                    }
                    _id--;
                    _entity = _it._entities[_id];
                    if (_it._excMask != null) {
                        if (_it._world.EntityCompatibleWithAndWithout (_entity, _it._incMask, _it._excMask)) {
                            return true;
                        }
                    } else {
                        if (_it._world.EntityCompatibleWith (_entity, _it._incMask)) {
                            return true;
                        }
                    }
                }
            }
        }

        internal void Init (ProtoWorld world, ProtoEntity[] entities, Slice<ulong> incMask, Slice<ulong> excMask, int from, int before) {
            _world = world;
            _entities = entities;
            _incMask = incMask;
            _excMask = excMask;
            _from = from;
            _before = before;
        }

        internal void Clear () {
            _world = default;
            _entities = default;
            _incMask = default;
            _from = default;
            _before = default;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class ProtoItExtensions {
        public static void RunParallel (this ProtoIt it, ProtoThreadHandler cb, int chunkSize, int workersLimit = 0) {
            it.Begin ();
#if DEBUG
            it.AddBlocker (1);
#endif
            var minPool = it.MinPool ();
            ThreadService.Run (cb, it.World (), minPool.Entities (), minPool.Len (), it.IncMask (), default, chunkSize, workersLimit);
#if DEBUG
            it.AddBlocker (-1);
#endif
            it.End ();
        }

        public static void RunParallel (this ProtoItExc it, ProtoThreadHandler cb, int chunkSize, int workersLimit = 0) {
            it.Begin ();
#if DEBUG
            it.AddBlocker (1);
#endif
            var minPool = it.MinPool ();
            ThreadService.Run (cb, it.World (), minPool.Entities (), minPool.Len (), it.IncMask (), it.ExcMask (), chunkSize, workersLimit);
#if DEBUG
            it.AddBlocker (-1);
#endif
            it.End ();
        }
    }
}
