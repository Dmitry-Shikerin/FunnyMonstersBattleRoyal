// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Ai.Goap {
    public struct GoapClaims {
        public GoapState Includes;
        public GoapState Excludes;
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class GoapClaimsExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static GoapClaims Inc (this in GoapClaims claims, int idx) {
            return new GoapClaims { Includes = claims.Includes.Set (idx), Excludes = claims.Excludes };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static GoapClaims Exc (this in GoapClaims claims, int idx) {
            return new GoapClaims { Includes = claims.Includes, Excludes = claims.Excludes.Set (idx) };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Compatible (this in GoapClaims claims, in GoapState state) {
            return (state.Value & claims.Includes.Value) == claims.Includes.Value &&
                   (state.Value & claims.Excludes.Value) == 0;
        }
    }

    public struct GoapState {
        public ulong Value;
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class GoapStateExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static GoapState Set (this in GoapState state, int idx) {
#if DEBUG
            if (idx < 0 || idx >= 64) { throw new Exception ($"некорректный индекс условия \"{idx}\", разрешенный диапазон [0,63]"); }
#endif
            return new GoapState { Value = state.Value | (1UL << idx) };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static GoapState Unset (this in GoapState state, int idx) {
#if DEBUG
            if (idx < 0 || idx >= 64) { throw new Exception ($"некорректный индекс условия \"{idx}\", разрешенный диапазон [0,63]"); }
#endif
            return new GoapState { Value = state.Value & ~(1UL << idx) };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Has (this in GoapState state, int idx) {
#if DEBUG
            if (idx < 0 || idx >= 64) { throw new Exception ($"некорректный индекс условия \"{idx}\", разрешенный диапазон [0,63]"); }
#endif
            return (state.Value & (1UL << idx)) != 0;
        }
    }

    public struct GoapActionState<T> {
        public T Id;
        public GoapState State;
    }

    public interface IGoapAction<T> {
        T Id ();
        GoapClaims OnInit (GoapPlanner<T> planner);
        GoapState OnExit (in GoapState state);
        float Cost () { return 1f; }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class GoapPlanner<T> {
        readonly List<Workspace> _wsPool;
        readonly object _wsPoolSync;
        readonly Workspace _wsBase;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public GoapPlanner (params IGoapAction<T>[] actions) {
            _wsPool = new (8);
            _wsPoolSync = new ();
            _wsBase = Workspace.New (this, actions);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Run (in GoapState from, in GoapClaims to, List<T> results) {
            var ws = GetWorkspace ();
            var valid = ws.Run (from, to, results);
            RecycleWorkspace (ws);
            return valid;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool RunWithState (in GoapState from, in GoapClaims to, List<GoapActionState<T>> results) {
            var ws = GetWorkspace ();
            var valid = ws.RunWithState (from, to, results);
            RecycleWorkspace (ws);
            return valid;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void RecycleWorkspace (Workspace ws) {
            lock (_wsPoolSync) {
                _wsPool.Add (ws);
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        Workspace GetWorkspace () {
            Workspace ws = default;
            lock (_wsPoolSync) {
                var idx = _wsPool.Count - 1;
                if (idx >= 0) {
                    ws = _wsPool[idx];
                    _wsPool.RemoveAt (idx);
                }
            }
            return ws ?? _wsBase.Clone ();
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
        sealed class Workspace {
            readonly Item[] _nodes;

            public static Workspace New (GoapPlanner<T> planner, IGoapAction<T>[] actions) {
                var ws = new Workspace (actions.Length + 2);
                for (var i = 0; i < actions.Length; i++) {
                    var action = actions[i];
                    ref var node = ref ws._nodes[i + 2];
                    node.ActionCached = action;
                    node.RequiresCached = action.OnInit (planner);
                    node.CostCached = action.Cost ();
                }
                return ws;
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            Workspace (int count) {
                _nodes = new Item[count];
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public Workspace Clone () {
                var clone = new Workspace (_nodes.Length);
                Array.Copy (_nodes, 0, clone._nodes, 0, _nodes.Length);
                return clone;
            }

            public bool Run (in GoapState from, in GoapClaims to, List<T> results) {
                RunInternal (from, to);
                results.Clear ();
                var prev = _nodes[1].Prev;
                if (prev == -1) {
                    return false;
                }
                while (prev != 0) {
                    results.Add (_nodes[prev].ActionCached.Id ());
                    prev = _nodes[prev].Prev;
                }
                results.Reverse ();
                return true;
            }

            public bool RunWithState (in GoapState from, in GoapClaims to, List<GoapActionState<T>> results) {
                RunInternal (from, to);
                results.Clear ();
                var prev = _nodes[1].Prev;
                if (prev == -1) {
                    return false;
                }
                while (prev != 0) {
                    ref var node = ref _nodes[prev];
                    results.Add (new GoapActionState<T> { Id = node.ActionCached.Id (), State = node.ExitState });
                    prev = _nodes[prev].Prev;
                }
                results.Reverse ();
                return true;
            }

            void RunInternal (in GoapState from, in GoapClaims to) {
                for (var i = 0; i < _nodes.Length; i++) {
                    ref var node = ref _nodes[i];
                    node.TotalCost = float.MaxValue;
                    node.ExitState = new GoapState ();
                    node.Prev = -1;
                    node.Visited = false;
                }
                // _nodes[0] - start
                // _nodes[1] - finish
                _nodes[0].RequiresCached = new GoapClaims { Includes = from };
                _nodes[0].TotalCost = 0f;
                _nodes[1].RequiresCached = to;
                while (true) {
                    var closest = GetClosest ();
                    if (closest == -1) {
                        break;
                    }
                    ref var node = ref _nodes[closest];
                    node.Visited = true;
                    node.ExitState = node.ActionCached?.OnExit (_nodes[node.Prev].ExitState) ?? node.RequiresCached.Includes;
                    ProcessNeighbors (closest);
                }
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            void ProcessNeighbors (int idx) {
                ref var currNode = ref _nodes[idx];
                for (var i = 0; i < _nodes.Length; i++) {
                    ref var nextNode = ref _nodes[i];
                    if (nextNode.Visited || !nextNode.RequiresCached.Compatible (currNode.ExitState)) {
                        continue;
                    }
                    var nextDist = currNode.TotalCost + nextNode.CostCached;
                    if (nextDist < nextNode.TotalCost) {
                        nextNode.TotalCost = nextDist;
                        nextNode.Prev = idx;
                    }
                }
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            int GetClosest () {
                var idx = -1;
                var closest = float.MaxValue;
                for (var i = 0; i < _nodes.Length; i++) {
                    ref var node = ref _nodes[i];
                    if (!node.Visited && node.TotalCost < closest) {
                        closest = node.TotalCost;
                        idx = i;
                    }
                }
                return idx;
            }

            struct Item {
                public IGoapAction<T> ActionCached;
                public GoapClaims RequiresCached;
                public float CostCached;
                public float TotalCost;
                public GoapState ExitState;
                public int Prev;
                public bool Visited;
            }
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
