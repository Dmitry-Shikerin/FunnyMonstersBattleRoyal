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

namespace Leopotam.Scripting {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ScriptVm {
        readonly Parser _parser;
        readonly Dictionary<int, ScriptFuncDecl> _funcs;
        readonly List<int> _unloadFuncs;

        ScriptFuncDecl[] _scriptFunctionPool;
        int _scriptFunctionPoolCount;

        ScriptVars[] _scriptVarsPool;
        int _scriptVarsPoolCount;

        ScriptArgs[] _scriptArgsPool;
        int _scriptArgsPoolCount;

        public ScriptVm () {
            _parser = new Parser (this);
            _funcs = new (128);
            _unloadFuncs = new (128);
            // ScriptFunction.
            _scriptFunctionPool = new ScriptFuncDecl[32];
            _scriptFunctionPoolCount = 0;
            // ScriptVars.
            _scriptVarsPool = new ScriptVars[32];
            _scriptVarsPoolCount = 0;
            // ScriptArgs.
            _scriptArgsPool = new ScriptArgs[32];
            _scriptArgsPoolCount = 0;
        }

        public string Load (string code) {
            var err = Unload (false);
            if (err != null) {
                return err;
            }
            ClearGlobals ();
            return _parser.Init (code).Parse ();
        }

        public string Unload (bool unloadHostedFuncs) {
            if (_parser.InRun ()) {
                return "нельзя выгружать код внутри вызова скрипта";
            }
            RemoveFuncs (false);
            if (unloadHostedFuncs) {
                RemoveFuncs (true);
            }
            _parser.Reset ();
            return null;
        }

        public bool IsLoaded () => _parser.IsParsed ();

        public bool InRun () => _parser.InRun ();

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (ScriptVar, string) Run (string fnName, ScriptArgs args) {
            if (_parser.InRun ()) {
                RecycleScriptArgs (args);
                return (default, "нельзя запускать функцию внутри запуска другой функции");
            }
            return _parser.Run (Hash (fnName), args);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptFuncDecl NewScriptFunction () {
            ScriptFuncDecl fn = null;
            if (_scriptFunctionPoolCount > 0) {
                fn = _scriptFunctionPool[--_scriptFunctionPoolCount];
#if DEBUG
                fn._recycled = false;
#endif
            }
            return fn ?? new ScriptFuncDecl ();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal void RecycleScriptFunction (ScriptFuncDecl fn) {
#if DEBUG
            if (fn._recycled) { throw new Exception ("двойное освобождение"); }
            fn._recycled = true;
#endif
            fn.Clear (this);
            if (_scriptFunctionPool.Length == _scriptFunctionPoolCount) {
                Array.Resize (ref _scriptFunctionPool, _scriptFunctionPoolCount << 1);
            }
            _scriptFunctionPool[_scriptFunctionPoolCount++] = fn;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptVars NewScriptVars (ScriptVars parent) {
            ScriptVars vars = null;
            if (_scriptVarsPoolCount > 0) {
                vars = _scriptVarsPool[--_scriptVarsPoolCount];
#if DEBUG
                vars._recycled = false;
#endif
            }
            vars ??= new ScriptVars ();
            vars.SetParent (parent);
            return vars;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal void RecycleScriptVars (ScriptVars vars) {
#if DEBUG
            if (vars._recycled) { throw new Exception ("двойное освобождение"); }
            vars._recycled = true;
#endif
            vars.Clear ();
            if (_scriptVarsPool.Length == _scriptVarsPoolCount) {
                Array.Resize (ref _scriptVarsPool, _scriptVarsPoolCount << 1);
            }
            _scriptVarsPool[_scriptVarsPoolCount++] = vars;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptArgs NewScriptArgs () {
            ScriptArgs args = null;
            if (_scriptArgsPoolCount > 0) {
                args = _scriptArgsPool[--_scriptArgsPoolCount];
#if DEBUG
                args._recycled = false;
#endif
            }
            return args ?? new ScriptArgs ();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal void RecycleScriptArgs (ScriptArgs args) {
#if DEBUG
            if (args._recycled) { throw new Exception ("двойное освобождение"); }
            args._recycled = true;
#endif
            args.Clear ();
            if (_scriptArgsPool.Length == _scriptArgsPoolCount) {
                Array.Resize (ref _scriptArgsPool, _scriptArgsPoolCount << 1);
            }
            _scriptArgsPool[_scriptArgsPoolCount++] = args;
        }

        public void ClearGlobals () {
            _parser.ClearGlobals ();
        }

        public void RemoveFuncs (bool hosted) {
            foreach (var kv in _funcs) {
                if (hosted ? kv.Value.Hosted != null : kv.Value.Hosted == null) {
                    RecycleScriptFunction (kv.Value);
                    _unloadFuncs.Add (kv.Key);
                }
            }
            foreach (var hash in _unloadFuncs) {
                _funcs.Remove (hash);
            }
            _unloadFuncs.Clear ();
        }

        public bool AddFunc (string name, HostFunctionHandler cb) {
            var nameHash = Hash (name);
            if (_funcs.ContainsKey (nameHash)) {
                return false;
            }
            var fn = NewScriptFunction ();
            fn.Hosted = cb;
            _funcs[nameHash] = fn;
            return true;
        }

        internal bool AddScriptFunc (int nameHash, int lexemIdx, ScriptArgs callArgs) {
            if (_funcs.ContainsKey (nameHash)) {
                RecycleScriptArgs (callArgs);
                return false;
            }
            var fn = NewScriptFunction ();
            fn.Scripted = lexemIdx;
            fn.ScriptedArgs = callArgs;
            _funcs[nameHash] = fn;
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal (ScriptFuncDecl, bool ok) FuncDecl (int nameHash) {
            return _funcs.TryGetValue (nameHash, out var fn) ? (fn, true) : (default, false);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Hash (string str) {
            return Lexer.IdentHash (str, 0, str.Length);
        }
    }
}
