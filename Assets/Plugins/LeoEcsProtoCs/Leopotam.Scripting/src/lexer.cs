// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using System;
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Scripting {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class Lexer {
        string _code;
        int _idx;
        int _col;
        int _line;
        char _ch;
        TokenType _lastToken;

        static int _hashRet = StrHash ("ret");
        static int _hashSet = StrHash ("set");
        static int _hashSetGlobal = StrHash ("setGlobal");
        static int _hashIf = StrHash ("if");
        static int _hashElse = StrHash ("else");
        static int _hashLoop = StrHash ("loop");

        public void Init (string code) {
            Reset ();
            _code = code;
            ReadChar ();
        }

        public void Reset () {
            _code = "";
            _idx = 0;
            _lastToken = TokenType.Eof;
            _line = 1;
            _col = 0;
            _ch = '\0';
        }

        public string Code () {
            return _code;
        }

        public Token NextToken () {
            if (SkipWs ()) {
                switch (_lastToken) {
                    case TokenType.Eof:
                    case TokenType.EndExpr:
                    case TokenType.Comma:
                    case TokenType.OpenBrace:
                    case TokenType.OpenParen:
                        ReadChar ();
                        return NextToken ();
                    default:
                        var endExprToken = Token.New (TokenType.EndExpr, _idx, _idx, _line, _col, 0, "", 0f);
                        _lastToken = TokenType.EndExpr;
                        ReadChar ();
                        return endExprToken;
                }
            }
            var row = _line;
            var col = _col;
            var start = _idx - 1;
            var identHash = 0;
            var constString = "";
            var constNumber = 0f;
            switch (_ch) {
                case '\0':
                    _lastToken = TokenType.Eof;
                    start = _idx;
                    break;
                case '#':
                    SkipComment ();
                    return NextToken ();
                case ',':
                    _lastToken = TokenType.Comma;
                    break;
                case '(':
                    _lastToken = TokenType.OpenParen;
                    break;
                case ')':
                    _lastToken = TokenType.CloseParen;
                    break;
                case '{':
                    _lastToken = TokenType.OpenBrace;
                    break;
                case '}':
                    _lastToken = TokenType.CloseBrace;
                    break;
                case '\'':
                case '"':
                    ReadString (_ch);
                    var len = _idx - start - 2;
                    if (len > 0) {
                        constString = _code.Substring (start + 1, len);
                    }
                    _lastToken = TokenType.String;
                    break;
                default:
                    if (IsNumber ()) {
                        constNumber = ReadNumber ();
                        _lastToken = TokenType.Number;
                    } else {
                        ReadIdent ();
                        identHash = IdentHash (_code, start, _idx);
                        _lastToken = CheckKeyword (identHash);
                    }
                    break;
            }
            var t = Token.New (_lastToken, start, _idx, row, col, identHash, constString, constNumber);
            ReadChar ();
            return t;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        TokenType CheckKeyword (int hash) {
            if (hash == _hashSet) { return TokenType.Set; }
            if (hash == _hashRet) { return TokenType.Ret; }
            if (hash == _hashIf) { return TokenType.If; }
            if (hash == _hashElse) { return TokenType.Else; }
            if (hash == _hashLoop) { return TokenType.Loop; }
            if (hash == _hashSetGlobal) { return TokenType.SetGlobal; }
            return TokenType.Ident;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void ReadIdent () {
            var c = PeekChar ();
            while (!char.IsWhiteSpace (c) && "(){},\"'".IndexOf (c) == -1) {
                _idx++;
                _col++;
                c = PeekChar ();
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void ReadString (char quote) {
            ReadChar ();
            while (_ch != quote && _ch != '\0') {
                ReadChar ();
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        bool IsNumber () {
            if (_ch == '-' && char.IsNumber (PeekChar ())) {
                return true;
            }
            return char.IsNumber (_ch);
        }

        float ReadNumber () {
            var sign = _ch == '-';
            if (sign) {
                ReadChar ();
            }
            var wholePart = _ch - '0';
            var fracPart = 0;
            var fracPartDiv = 1;
            var dotFound = false;
            while (true) {
                var c = PeekChar ();
                if (c == '.') {
                    if (dotFound) {
                        break;
                    }
                    dotFound = true;
                    _idx++;
                    _col++;
                    continue;
                }
                if (!char.IsDigit (c)) {
                    break;
                }
                if (dotFound) {
                    // дробная часть.
                    fracPart = fracPart * 10 + (c - '0');
                    fracPartDiv *= 10;
                } else {
                    // целая часть.
                    wholePart = wholePart * 10 + (c - '0');
                }
                _idx++;
                _col++;
            }
            var res = wholePart + fracPart / (float) fracPartDiv;
            return sign ? -res : res;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void ReadChar () {
            if (_idx < _code.Length) {
                _ch = _code[_idx];
                _idx++;
                _col++;
                if (_ch == '\n') {
                    _line++;
                    _col = 0;
                }
            } else {
                _ch = '\0';
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        char PeekChar () {
            return _idx < _code.Length ? _code[_idx] : '\0';
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        bool SkipWs () {
            while (_ch == ' ' || _ch == '\t' || _ch == '\r') {
                ReadChar ();
            }
            return _ch == '\n';
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void SkipComment () {
            while (_ch != '\n' && _ch != '\0') {
                ReadChar ();
            }
            ReadChar ();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        static int StrHash (string str) {
            return IdentHash (str, 0, str.Length);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int IdentHash (string str, int start, int end) {
            unchecked {
                var hash = 23;
                for (var i = start; i < end; i++) {
                    hash = hash * 31 + str[i];
                }
                return hash;
            }
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public struct Token {
        public TokenType Type;
        public int Column;
        public int Row;
        public int Start;
        public int End;
        public int IdentHash;
        public string ConstString;
        public float ConstNumber;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static Token New (TokenType type, int start, int end, int row, int col, int identHash, string constString, float constNumber) {
            return new () {
                Type = type,
                Start = start,
                End = end,
                Row = row,
                Column = col,
                IdentHash = identHash,
                ConstString = constString,
                ConstNumber = constNumber,
            };
        }
    }

    public enum TokenType {
        Eof,
        EndExpr,
        Ident,
        Number,
        String,
        OpenParen,
        CloseParen,
        OpenBrace,
        CloseBrace,
        Comma,
        Ret,
        Set,
        SetGlobal,
        If,
        Else,
        Loop,
    }
}
