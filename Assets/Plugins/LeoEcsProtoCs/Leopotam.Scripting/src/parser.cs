// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Scripting {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class Parser {
        readonly ScriptVm _vm;
        readonly Lexer _lexer;
        Token[] _lexems;
        int _lexemsCount;
        int _lexemIdx;
        Token _currToken;
        Token _peekToken;
        bool _parsed;
        bool _inRun;
        ScriptVars _globalVars;
        ScriptVars _localVars;

        public Parser (ScriptVm vm) {
            _vm = vm;
            _lexer = new Lexer ();
            _lexems = new Token[512];
            _globalVars = _vm.NewScriptVars (null);
            Reset ();
        }

        public bool IsParsed () => _parsed;

        public bool InRun () => _inRun;

        public Parser Init (string code) {
            Reset ();
            _lexer.Init (code);
            return this;
        }

        public string Parse () {
            if (_parsed) { return null; }
            var t = _lexer.NextToken ();
            string err = null;
            var code = _lexer.Code ();
            while (t.Type != TokenType.Eof) {
                // валидация строк на кавычки.
                if (t.Type == TokenType.String) {
                    if (code[t.Start] != code[t.End - 1]) { return "незакрытая строка"; }
                    if (code[t.Start] == '"') {
                        var found = false;
                        for (var i = t.Start + 1; i < t.End - 1; i++) {
                            if (code[i] == '\'') {
                                found = true;
                                break;
                            }
                        }
                        if (!found) {
                            return $"[{t.Row}:{t.Column}] двойные кавычки разрешены только для строк, содержащих одинарные кавычки";
                        }
                    }
                }
                if (_lexemsCount == _lexems.Length) {
                    Array.Resize (ref _lexems, _lexemsCount << 1);
                }
                _lexems[_lexemsCount++] = t;
                t = _lexer.NextToken ();
            }
            SetTokenPos (0);
            while (err == null && _currToken.Type != TokenType.Eof) {
                err = ParseFunction ();
            }
            _parsed = err == null;
            return err;
        }

        public (ScriptVar, string) Run (int nameHash, ScriptArgs args) {
            args ??= _vm.NewScriptArgs ();
            if (!_parsed) {
                _vm.RecycleScriptArgs (args);
                return (default, "код содержит ошибки");
            }
            var (fn, ok) = _vm.FuncDecl (nameHash);
            if (!ok) {
                _vm.RecycleScriptArgs (args);
                return (default, "функция не существует");
            }
            _inRun = true;
            var (v, err) = CallInternal (fn, args);
            _inRun = false;
            return (v, err);
        }

        public void ClearGlobals () {
            _globalVars.Clear ();
        }

        public void Reset () {
            _parsed = false;
            _inRun = false;
            _lexer.Reset ();
            _lexemsCount = 0;
            _currToken = default;
            _peekToken = default;
            _globalVars.Clear ();
        }

        string ParseFunction () {
            string err;
            err = Expect (TokenType.Ident, false);
            if (err != null) { return err; }
            var fnName = _currToken.IdentHash;
            NextToken ();
            err = Expect (TokenType.OpenParen, true);
            if (err != null) { return err; }
            var callArgs = _vm.NewScriptArgs ();
            ScriptVar res;
            if (_currToken.Type != TokenType.CloseParen && _currToken.Type != TokenType.Eof) {
                // параметры.
                while (true) {
                    err = Expect (TokenType.Ident, false);
                    if (err != null) {
                        _vm.RecycleScriptArgs (callArgs);
                        return err;
                    }
                    res = ScriptVar.NewVariable (_currToken.IdentHash);
                    callArgs.Add (res);
                    NextToken ();
                    if (_currToken.Type == TokenType.CloseParen || _currToken.Type == TokenType.Eof) {
                        // конец списка.
                        break;
                    }
                    err = Expect (TokenType.Comma, true);
                    if (err != null) {
                        _vm.RecycleScriptArgs (callArgs);
                        return err;
                    }
                }
            }
            err = Expect (TokenType.CloseParen, true);
            if (err != null) { return err; }
            if (!_vm.AddScriptFunc (fnName, _lexemIdx - 3 + 1, callArgs)) {
                return NewError ($"функция с таким именем уже существует");
            }
            (_, _, err) = ParseFunctionBody ();
            if (err != null) { return err; }
            return ExpectOrEof (TokenType.EndExpr, true);
        }

        (ScriptVar, bool, string) ParseFunctionBody () {
            string err;
            err = Expect (TokenType.OpenBrace, true);
            if (err != null) { return (default, false, err); }
            var valid = true;
            var retVal = ScriptVar.NewUndef ();
            bool withRet;
            while (valid) {
                switch (_currToken.Type) {
                    case TokenType.If:
                        (retVal, withRet, err) = ParseIf ();
                        if (err != null) { return (default, false, err); }
                        if (_parsed && withRet) { return (retVal, true, null); }
                        // TODO: перенести в ParseIf.
                        err = Expect (TokenType.EndExpr, true);
                        if (err != null) { return (default, false, err); }
                        break;
                    case TokenType.Loop:
                        (retVal, withRet, err) = ParseLoop ();
                        if (err != null) { return (default, false, err); }
                        if (_parsed && withRet) { return (retVal, true, null); }
                        // TODO: перенести в ParseFor.
                        err = Expect (TokenType.EndExpr, true);
                        if (err != null) { return (default, false, err); }
                        break;
                    case TokenType.Ident:
                        (_, err) = ParseCall ();
                        if (err != null) { return (default, false, err); }
                        err = Expect (TokenType.EndExpr, true);
                        if (err != null) { return (default, false, err); }
                        break;
                    case TokenType.Ret:
                        (retVal, err) = ParseRet ();
                        if (_parsed) { return (retVal, true, err); }
                        if (err != null) { return (default, false, err); }
                        break;
                    case TokenType.Set:
                        err = ParseSet ();
                        if (err != null) { return (default, false, err); }
                        break;
                    case TokenType.SetGlobal:
                        err = ParseSetGlobal ();
                        if (err != null) { return (default, false, err); }
                        break;
                    default:
                        valid = false;
                        break;
                }
                valid &= _currToken.Type != TokenType.CloseBrace && _currToken.Type != TokenType.Eof;
            }
            err = Expect (TokenType.CloseBrace, true);
            return (retVal, false, err);
        }

        (ScriptVar, bool, string) ParseLoop () {
            string err;
            // bool withRet;
            err = Expect (TokenType.Loop, true);
            if (err != null) { return (default, false, err); }
            err = Expect (TokenType.Ident, false);
            if (err != null) { return (default, false, err); }
            var varName = _currToken.IdentHash;
            NextToken ();
            ScriptVar startVal;
            ScriptVar retVal;
            (startVal, err) = ParseExpr ();
            if (err != null) { return (default, false, err); }
            err = Expect (TokenType.Comma, true);
            if (err != null) { return (default, false, err); }
            (retVal, err) = ParseExpr ();
            if (err != null) { return (default, false, err); }
            if (_parsed) {
                UnwrapVar (ref startVal);
                UnwrapVar (ref retVal);
                var (num1F, numOk1) = startVal.NumValue ();
                var (num2F, numOk2) = retVal.NumValue ();
                if (!numOk1 || !numOk2) {
                    return (default, false, NewError ("loop работает только с числами"));
                }
                var loopStart = (int) Math.Round (num1F);
                var loopEnd = (int) Math.Round (num2F);
                var loopStep = loopStart > loopEnd ? -1 : 1;
                _localVars.Set (varName, ScriptVar.NewNum (loopStart));
                var oldLexIdx = _lexemIdx;
                var oldCurrToken = _currToken;
                var oldPeekToken = _peekToken;
                bool withRet;
                while (loopStart != loopEnd) {
                    _lexemIdx = oldLexIdx;
                    _currToken = oldCurrToken;
                    _peekToken = oldPeekToken;
                    (retVal, withRet, err) = ParseFunctionBody ();
                    if (err != null || withRet) { return (retVal, withRet, err); }
                    loopStart += loopStep;
                    _localVars.Set (varName, ScriptVar.NewNum (loopStart));
                }
            } else {
                (_, _, err) = ParseFunctionBody ();
                if (err != null) { return (default, false, err); }
            }
            return (default, false, null);
        }

        (ScriptVar, bool, string) ParseIf () {
            string err;
            bool withRet;
            err = Expect (TokenType.If, true);
            if (err != null) { return (default, false, err); }
            ScriptVar retVal;
            (retVal, err) = ParseExpr ();
            if (err != null) { return (default, false, err); }
            var exprTrue = false;
            if (_parsed) {
                UnwrapVar (ref retVal);
                var (num, numOk) = retVal.NumValue ();
                if (!numOk) {
                    return (default, false, NewError ("некорректный результат выражения"));
                }
                exprTrue = num > 0.0001f || num < -0.0001f;
                if (exprTrue) {
                    (retVal, withRet, err) = ParseFunctionBody ();
                    if (err != null || withRet) { return (retVal, withRet, err); }
                } else {
                    _parsed = false;
                    ParseFunctionBody ();
                    _parsed = true;
                }
            } else {
                (_, _, err) = ParseFunctionBody ();
                if (err != null) { return (default, false, err); }
            }
            if (_currToken.Type != TokenType.Else) {
                return (default, false, null);
            }
            NextToken ();
            if (_parsed) {
                if (exprTrue) {
                    ParseFunctionBody ();
                } else {
                    (retVal, withRet, err) = ParseFunctionBody ();
                    if (err != null || withRet) { return (retVal, withRet, err); }
                }
            } else {
                (_, _, err) = ParseFunctionBody ();
            }
            return (default, false, err);
        }

        (ScriptVar, string) ParseRet () {
            string err;
            err = Expect (TokenType.Ret, true);
            if (err != null) { return (default, err); }
            ScriptVar retVal;
            (retVal, err) = ParseExpr ();
            if (err != null) { return (default, err); }
            UnwrapVar (ref retVal);
            return (retVal, Expect (TokenType.EndExpr, true));
        }

        string ParseSet () {
            string err;
            err = Expect (TokenType.Set, true);
            if (err != null) { return err; }
            err = Expect (TokenType.Ident, false);
            if (err != null) { return err; }
            var varName = _currToken.IdentHash;
            NextToken ();
            ScriptVar retVal;
            (retVal, err) = ParseExpr ();
            if (err != null) { return err; }
            if (_parsed) {
                UnwrapVar (ref retVal);
                _localVars.Set (varName, retVal);
            }
            return Expect (TokenType.EndExpr, true);
        }

        string ParseSetGlobal () {
            string err;
            err = Expect (TokenType.SetGlobal, true);
            if (err != null) { return err; }
            err = Expect (TokenType.Ident, false);
            if (err != null) { return err; }
            var varName = _currToken.IdentHash;
            NextToken ();
            ScriptVar retVal;
            (retVal, err) = ParseExpr ();
            if (err != null) { return err; }
            if (_parsed) {
                _globalVars.Set (varName, retVal);
            }
            return Expect (TokenType.EndExpr, true);
        }

        (ScriptVar, string) ParseCall () {
            string err;
            err = Expect (TokenType.Ident, false);
            if (err != null) { return (default, err); }
            var fnName = _currToken.IdentHash;
            ScriptFuncDecl fn = default;
            ScriptArgs callArgs = null;
            bool fnOk;
            if (_parsed) {
                (fn, fnOk) = _vm.FuncDecl (fnName);
                if (!fnOk) {
                    return (default, NewError ("функция с таким именем не найдена"));
                }
                callArgs = _vm.NewScriptArgs ();
            }
            NextToken ();
            err = Expect (TokenType.OpenParen, true);
            if (err != null) {
                if (_parsed) { _vm.RecycleScriptArgs (callArgs); }
                return (default, err);
            }
            var res = ScriptVar.NewUndef ();
            if (_currToken.Type != TokenType.CloseParen && _currToken.Type != TokenType.Eof) {
                // параметры.
                while (true) {
                    (res, err) = ParseExpr ();
                    if (err != null) {
                        if (_parsed) { _vm.RecycleScriptArgs (callArgs); }
                        return (default, err);
                    }
                    if (callArgs != null) {
                        var (resVar, resIsVar) = res.VarValue ();
                        if (resIsVar) {
                            if (!_localVars.Has (resVar)) {
                                if (_parsed) { _vm.RecycleScriptArgs (callArgs); }
                                return (default, NewError ("неизвестная переменная"));
                            }
                            // развертывание переменной в ее значение.
                            res = _localVars.Get (resVar);
                        }
                        callArgs.Add (res);
                    }
                    if (_currToken.Type == TokenType.CloseParen || _currToken.Type == TokenType.Eof) {
                        // конец списка.
                        break;
                    }
                    err = Expect (TokenType.Comma, true);
                    if (err != null) {
                        if (_parsed) { _vm.RecycleScriptArgs (callArgs); }
                        return (default, err);
                    }
                }
            }
            if (callArgs != null) {
                (res, err) = CallInternal (fn, callArgs);
                if (err != null) { return (default, NewError (err)); }
            }
            return (res, Expect (TokenType.CloseParen, true));
        }

        (ScriptVar, string) CallInternal (ScriptFuncDecl fn, ScriptArgs scriptArgs) {
            ScriptVar retVal;
            string err;
            if (fn.Hosted != null) {
                // хост-функция.
                (retVal, err) = fn.Hosted (scriptArgs);
            } else {
                // скрипт-функция.
                var locals = _vm.NewScriptVars (_globalVars);
                for (int i = 0, iMax = fn.ScriptedArgs.Count (); i < iMax; i++) {
                    locals.Set (fn.ScriptedArgs.Get (i).VarValue ().Item1, scriptArgs.Get (i));
                }
                var oldLocals = _localVars;
                var oldLexIdx = _lexemIdx;
                var oldCurrToken = _currToken;
                var oldPeekToken = _peekToken;
                _localVars = locals;
                SetTokenPos (fn.Scripted);
                (retVal, _, err) = ParseFunctionBody ();
                _lexemIdx = oldLexIdx;
                _currToken = oldCurrToken;
                _peekToken = oldPeekToken;
                _localVars = oldLocals;
                _vm.RecycleScriptVars (locals);
            }
            _vm.RecycleScriptArgs (scriptArgs);
            return (retVal, err);
        }

        (ScriptVar, string) ParseExpr () {
            ScriptVar retVal;
            string err;
            switch (_currToken.Type) {
                case TokenType.Number:
                    retVal = ScriptVar.NewNum (_currToken.ConstNumber);
                    NextToken ();
                    return (retVal, null);
                case TokenType.String:
                    retVal = ScriptVar.NewStr (_currToken.ConstString);
                    NextToken ();
                    return (retVal, null);
            }
            err = Expect (TokenType.Ident, false);
            if (err != null) { return (default, err); }
            // LA1: x(...
            if (_peekToken.Type == TokenType.OpenParen) {
                return ParseCall ();
            }
            retVal = ScriptVar.NewVariable (_currToken.IdentHash);
            NextToken ();
            return (retVal, null);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void UnwrapVar (ref ScriptVar v) {
            if (_parsed) {
                var (vv, ok) = v.VarValue ();
                if (ok) {
                    v = _localVars.Get (vv);
                }
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        string Expect (TokenType tokenType, bool next) {
            if (_currToken.Type != tokenType) {
                return ExpectError (tokenType, _currToken);
            }
            if (next) {
                NextToken ();
            }
            return null;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        string ExpectOrEof (TokenType tokenType, bool next) {
            if (_currToken.Type == TokenType.Eof) { return null; }
            return Expect (tokenType, next);
        }

        string ExpectError (TokenType expect, in Token actual) {
            var actualStr = _lexer.Code ().Substring (actual.Start, actual.End - actual.Start);
            return NewError ($"ожидалось: \"{TokenUserName (expect)}\", получено: \"{actualStr}\"");
        }

        string NewError (string msg) {
            return $"[{_currToken.Row}:{_currToken.Column}] {msg}";
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void NextToken () {
            _currToken = _peekToken;
            if (_lexemIdx < _lexemsCount) {
                _peekToken = _lexems[_lexemIdx++];
            } else {
                _peekToken = Token.New (TokenType.Eof, 0, 0, 0, 0, 0, "", 0f);
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void SetTokenPos (int idx) {
            if (idx >= 0 && idx < _lexemsCount) {
                _lexemIdx = idx;
                NextToken ();
                NextToken ();
            }
        }

        string TokenUserName (TokenType tokenType) {
            switch (tokenType) {
                case TokenType.EndExpr:
                    return "конец выражения";
                case TokenType.OpenParen:
                    return "(";
                case TokenType.CloseParen:
                    return ")";
                case TokenType.OpenBrace:
                    return "{";
                case TokenType.CloseBrace:
                    return "}";
                case TokenType.Comma:
                    return ",";
                case TokenType.Ret:
                    return "return";
                case TokenType.Ident:
                    return "идентификатор";
                case TokenType.Number:
                    return "число";
                case TokenType.String:
                    return "строка";
                default:
                    return tokenType.ToString ();
            }
        }
    }
}
