// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Leopotam.SimpleBinary {
    public sealed class ListPool<T> {
        static List<T>[] _pool;
        static int _poolCount;

        static ListPool () {
            _pool = new List<T>[16];
            _poolCount = 0;
        }

        public static List<T> Get () {
            if (_poolCount > 0) {
                return _pool[--_poolCount];
            }
            return new List<T> ();
        }

        public static void Recycle (List<T> item) {
            if (item != null) {
                item.Clear ();
                if (_pool.Length == _poolCount) {
                    Array.Resize (ref _pool, _poolCount << 1);
                }
                _pool[_poolCount++] = item;
            }
        }
    }

    public sealed class ListPoolThreaded<T> {
        static List<T>[] _pool;
        static int _poolCount;
        static readonly object _sync;

        static ListPoolThreaded () {
            _pool = new List<T>[16];
            _poolCount = 0;
            _sync = new object ();
        }

        public static List<T> Get () {
            lock (_sync) {
                if (_poolCount > 0) {
                    return _pool[--_poolCount];
                }
            }
            return new List<T> ();
        }

        public static void Recycle (List<T> item) {
            item.Clear ();
            lock (_sync) {
                if (_pool.Length == _poolCount) {
                    Array.Resize (ref _pool, _poolCount << 1);
                }
                _pool[_poolCount++] = item;
            }
        }
    }

    public struct SimpleBinaryReader {
        readonly byte[] _data;
        readonly int _cap;
        int _offset;

        public SimpleBinaryReader (byte[] data, int offset = 0) {
            _data = data;
            _cap = _data.Length;
            _offset = offset;
        }

        public (byte[], int) GetBuffer () {
            return (_data, _offset);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (ushort, bool) PeekPacketType () {
            if (!CheckCapacity (2)) { return (default, false); }
            return ((ushort) (_data[_offset] | (uint) _data[_offset + 1] << 8), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (byte, bool) ReadU8 () {
            if (!CheckCapacity (1)) { return (default, false); }
            return (ReadU8Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public byte ReadU8Unchecked () {
            var v = _data[_offset];
            _offset++;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (sbyte, bool) ReadI8 () {
            if (!CheckCapacity (1)) { return (default, false); }
            return (ReadI8Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public sbyte ReadI8Unchecked () {
            var v = (sbyte) _data[_offset];
            _offset++;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (ushort, bool) ReadU16 () {
            if (!CheckCapacity (2)) { return (default, false); }
            return (ReadU16Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ushort ReadU16Unchecked () {
            var v = (ushort) (_data[_offset] | (uint) _data[_offset + 1] << 8);
            _offset += 2;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (short, bool) ReadI16 () {
            if (!CheckCapacity (2)) { return (default, false); }
            return (ReadI16Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public short ReadI16Unchecked () {
            var v = (short) (_data[_offset] | _data[_offset + 1] << 8);
            _offset += 2;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (uint, bool) ReadU32 () {
            if (!CheckCapacity (4)) { return (default, false); }
            return (ReadU32Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public uint ReadU32Unchecked () {
            var v = (uint) (
                _data[_offset]
                | _data[_offset + 1] << 8
                | _data[_offset + 2] << 16
                | _data[_offset + 3] << 24);
            _offset += 4;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (int, bool) ReadI32 () {
            if (!CheckCapacity (4)) { return (default, false); }
            return (ReadI32Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int ReadI32Unchecked () {
            var v =
                _data[_offset]
                | _data[_offset + 1] << 8
                | _data[_offset + 2] << 16
                | _data[_offset + 3] << 24;
            _offset += 4;
            return v;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (float, bool) ReadF32 () {
            if (!CheckCapacity (4)) { return (default, false); }
            return (ReadF32Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public float ReadF32Unchecked () {
            F32Converter conv = default;
            conv.Uint = ReadU32Unchecked ();
            return conv.Float;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (double, bool) ReadF64 () {
            if (!CheckCapacity (8)) { return (default, false); }
            return (ReadF64Unchecked (), true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public double ReadF64Unchecked () {
            F64Converter conv = default;
            conv.Ulong1 = _data[_offset];
            conv.Ulong2 = _data[_offset + 1];
            conv.Ulong3 = _data[_offset + 2];
            conv.Ulong4 = _data[_offset + 3];
            conv.Ulong5 = _data[_offset + 4];
            conv.Ulong6 = _data[_offset + 5];
            conv.Ulong7 = _data[_offset + 6];
            conv.Ulong8 = _data[_offset + 7];
            _offset += 8;
            return conv.Double;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (string, bool) ReadS16 () {
            var (len, ok) = ReadU16 ();
            if (!ok) { return (default, false); }
            if (!CheckCapacity (len)) { return (default, false); }
            var v = Encoding.UTF8.GetString (_data, _offset, len);
            _offset += len;
            return (v, true);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        bool CheckCapacity (int space) {
            return _cap >= _offset + space;
        }
    }

    public struct SimpleBinaryWriter {
        byte[] _data;
        int _cap;
        int _offset;

        public SimpleBinaryWriter (byte[] data, int offset = 0) {
            _data = data;
            _cap = _data.Length;
            _offset = offset;
        }

        public (byte[], int) GetBuffer () {
            return (_data, _offset);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteU8 (byte v) {
            ValidateCapacity (1);
            _data[_offset] = v;
            _offset++;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteI8 (sbyte v) {
            ValidateCapacity (1);
            _data[_offset] = (byte) v;
            _offset++;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteU16 (ushort v) {
            ValidateCapacity (2);
            _data[_offset] = (byte) v;
            _data[_offset + 1] = (byte) ((uint) v >> 8);
            _offset += 2;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteI16 (short v) {
            ValidateCapacity (2);
            _data[_offset] = (byte) v;
            _data[_offset + 1] = (byte) ((uint) v >> 8);
            _offset += 2;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteU32 (uint v) {
            ValidateCapacity (4);
            _data[_offset] = (byte) v;
            _data[_offset + 1] = (byte) (v >> 8);
            _data[_offset + 2] = (byte) (v >> 16);
            _data[_offset + 3] = (byte) (v >> 24);
            _offset += 4;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteI32 (int v) {
            ValidateCapacity (4);
            _data[_offset] = (byte) v;
            _data[_offset + 1] = (byte) (v >> 8);
            _data[_offset + 2] = (byte) (v >> 16);
            _data[_offset + 3] = (byte) (v >> 24);
            _offset += 4;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteF32 (float v) {
            ValidateCapacity (4);
            F32Converter conv = default;
            conv.Float = v;
            WriteU32 (conv.Uint);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteF64 (double v) {
            ValidateCapacity (8);
            F64Converter conv = default;
            conv.Double = v;
            _data[_offset] = conv.Ulong1;
            _data[_offset + 1] = conv.Ulong2;
            _data[_offset + 2] = conv.Ulong3;
            _data[_offset + 3] = conv.Ulong4;
            _data[_offset + 4] = conv.Ulong5;
            _data[_offset + 5] = conv.Ulong6;
            _data[_offset + 6] = conv.Ulong7;
            _data[_offset + 7] = conv.Ulong8;
            _offset += 8;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void WriteS16 (string v) {
            var b = Encoding.UTF8.GetBytes (v);
            var len = b.Length;
            if (len > ushort.MaxValue) {
                len = ushort.MaxValue;
            }
            ValidateCapacity (2 + len);
            WriteU16 ((ushort) len);
            Array.Copy (b, 0, _data, _offset, len);
            _offset += len;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        void ValidateCapacity (int space) {
            if (_cap < _offset + space) {
                _cap = _data.Length << 1;
                Array.Resize (ref _data, _cap);
            }
        }
    }

    [StructLayout (LayoutKind.Explicit)]
    struct F32Converter {
        [FieldOffset (0)]
        public float Float;
        [FieldOffset (0)]
        public uint Uint;
    }

    [StructLayout (LayoutKind.Explicit)]
    struct F64Converter {
        [FieldOffset (0)]
        public double Double;
        [FieldOffset (0)]
        public byte Ulong1;
        [FieldOffset (1)]
        public byte Ulong2;
        [FieldOffset (2)]
        public byte Ulong3;
        [FieldOffset (3)]
        public byte Ulong4;
        [FieldOffset (4)]
        public byte Ulong5;
        [FieldOffset (5)]
        public byte Ulong6;
        [FieldOffset (6)]
        public byte Ulong7;
        [FieldOffset (7)]
        public byte Ulong8;
    }
}
