// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Threading;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.Threads {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    static class ThreadService {
        static ThreadDesc[] _descs;
        static readonly int _descsCount;
        static ProtoThreadHandler _task;
        static ProtoEntity[] _entities;
        static Slice<ulong> _incMask;
        static Slice<ulong> _excMask;
        static ProtoWorld _world;
        static ProtoThreadIt _mainThreadIt;

        static ThreadService () {
#if UNITY_WEBGL
            _descsCount = 1;
#else
            _descsCount = Environment.ProcessorCount;
#endif
            _descs = new ThreadDesc[_descsCount - 1];
            for (var i = 0; i < _descs.Length; i++) {
                var desc = new ThreadDesc ();
                _descs[i] = desc;
                desc.Thread = new Thread (ThreadProc) { IsBackground = true };
                desc.HasWork = new ManualResetEvent (false);
                desc.WorkDone = new ManualResetEvent (true);
                desc.Thread.Start (i);
            }
            _mainThreadIt = new ProtoThreadIt (_descs.Length);
        }

        public static void Run (ProtoThreadHandler cb, ProtoWorld world, ProtoEntity[] entities, int count, Slice<ulong> incMask, Slice<ulong> excMask, int chunkSize, int workersLimit) {
#if DEBUG
            if (_task != null) { throw new Exception ("[ProtoThreads] текущая задача не завершена, множественные вызовы не поддерживаются"); }
#endif
            if (count <= 0 || chunkSize <= 0) {
                return;
            }
            if (workersLimit <= 0 || workersLimit > _descsCount) {
                workersLimit = _descsCount;
            }
            _task = cb;
            _entities = entities;
            _world = world;
            _incMask = incMask;
            _excMask = excMask;
            var processed = 0;
            var jobSize = count / workersLimit;
            int workersCount;
            if (jobSize >= chunkSize) {
                workersCount = workersLimit;
            } else {
                workersCount = count / chunkSize;
                jobSize = chunkSize;
            }
            if (workersCount <= 0) {
                workersCount = 1;
            }
            for (int i = 0, iMax = workersCount - 1; i < iMax; i++) {
                var desc = _descs[i];
                desc.FromIndex = processed;
                processed += jobSize;
                desc.BeforeIndex = processed;
                desc.WorkDone.Reset ();
                desc.HasWork.Set ();
            }

            _mainThreadIt.Init (_world, _entities, _incMask, _excMask, processed, count);
            _task.Invoke (_mainThreadIt);
            _mainThreadIt.Clear ();

            for (int i = 0, iMax = workersCount - 1; i < iMax; i++) {
                _descs[i].WorkDone.WaitOne ();
            }

            _incMask = default;
            _excMask = default;
            _entities = default;
            _world = default;
            _task = default;
        }

        static void ThreadProc (object raw) {
            var workerId = (int) raw;
            var desc = _descs[workerId];
            var it = new ProtoThreadIt (workerId);
            try {
                while (Thread.CurrentThread.IsAlive) {
                    desc.HasWork.WaitOne ();
                    desc.HasWork.Reset ();
                    it.Init (_world, _entities, _incMask, _excMask, desc.FromIndex, desc.BeforeIndex);
                    _task.Invoke (it);
                    it.Clear ();
                    // UnityEngine.Debug.Log ($"[THREAD-{(int) raw}] [{desc.FromIndex};{desc.BeforeIndex}), len: {desc.BeforeIndex - desc.FromIndex}");
                    desc.WorkDone.Set ();
                }
            } catch (Exception ex) {
#if DEBUG
                if (!(ex is ThreadAbortException)) {
                    throw new Exception ($"[ProtoThreads] ошибка в пользовательском коде: {ex}");
                }
#endif
            }
        }

        sealed class ThreadDesc {
            public Thread Thread;
            public ManualResetEvent HasWork;
            public ManualResetEvent WorkDone;
            public int FromIndex;
            public int BeforeIndex;
        }
    }

    public delegate void ProtoThreadHandler (ProtoThreadIt it);
}
