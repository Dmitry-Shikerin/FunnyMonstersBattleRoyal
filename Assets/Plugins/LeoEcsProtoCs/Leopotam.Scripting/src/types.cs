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
    public delegate (ScriptVar, string) HostFunctionHandler (ScriptArgs args);

    public enum ScriptVarType {
        Undefined = 0,
        String,
        Number,
        Variable,
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public struct ScriptVar {
        ScriptVarType _type;
        float _asNumber;
        string _asString;
        int _asVariable;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ScriptVar NewUndef () {
            return new () { _type = ScriptVarType.Undefined };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ScriptVar NewNum (float data) {
            return new () { _type = ScriptVarType.Number, _asNumber = data };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ScriptVar NewStr (string data) {
            return new () { _type = ScriptVarType.String, _asString = data };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal static ScriptVar NewVariable (int identHash) {
            return new () { _type = ScriptVarType.Variable, _asVariable = identHash };
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptVarType TypeValue () => _type;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (float, bool) NumValue () => (_asNumber, _type == ScriptVarType.Number);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (string, bool) StrValue () => (_asString ?? "", _type == ScriptVarType.String);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal (int, bool) VarValue () => (_asVariable, _type == ScriptVarType.Variable);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsUndef () => _type == ScriptVarType.Undefined;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsNum () => _type == ScriptVarType.Number;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsStr () => _type == ScriptVarType.String;
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ScriptFuncDecl {
        public int Scripted;
        public ScriptArgs ScriptedArgs;
        public HostFunctionHandler Hosted;
#if DEBUG
        internal bool _recycled;
#endif

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptFuncDecl () {
            Scripted = 0;
            ScriptedArgs = null;
            Hosted = null;
#if DEBUG
            _recycled = false;
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear (ScriptVm vm) {
            Scripted = 0;
            if (ScriptedArgs != null) {
                vm.RecycleScriptArgs (ScriptedArgs);
                ScriptedArgs = null;
            }
            Hosted = null;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    sealed class ScriptVars {
        Dictionary<int, ScriptVar> _vars;
        ScriptVars _parent;
#if DEBUG
        internal bool _recycled;
#endif
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptVars () {
            _vars = new (32);
            _parent = null;
#if DEBUG
            _recycled = false;
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void SetParent (ScriptVars parent) {
            _parent = parent;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptVars Parent () => _parent;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int Count () => _vars.Count;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear () {
            _vars.Clear ();
            _parent = null;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Set (int hash, ScriptVar v) {
            _vars[hash] = v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (int hash) => _vars.ContainsKey (hash);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptVar Get (int hash) {
            if (_vars.TryGetValue (hash, out var v)) {
                return v;
            }
            return _parent?.Get (hash) ?? ScriptVar.NewUndef ();
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class ScriptArgs {
        ScriptVar[] _items;
        int _itemsCount;
#if DEBUG
        internal bool _recycled;
#endif
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptArgs () {
            _items = new ScriptVar[32];
            _itemsCount = 0;
#if DEBUG
            _recycled = false;
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear () {
            _itemsCount = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        internal ScriptArgs Add (ScriptVar v) {
            if (_itemsCount == _items.Length) {
                Array.Resize (ref _items, _itemsCount << 1);
            }
            _items[_itemsCount++] = v;
            return this;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptArgs AddNum (float v) {
            return Add (ScriptVar.NewNum (v));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptArgs AddStr (string v) {
            return Add (ScriptVar.NewStr (v));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int Count () => _itemsCount;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ScriptVar Get (int idx) {
            return idx >= 0 && idx < _itemsCount ? _items[idx] : ScriptVar.NewUndef ();
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
