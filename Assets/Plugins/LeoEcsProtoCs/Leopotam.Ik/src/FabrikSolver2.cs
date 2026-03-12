// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.Ik {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class FabrikSolver2 {
        FabrikSolver2Segment[] _segs;
        int _segsLen;
        float _basePosX;
        float _basePosY;
        float _baseDirX;
        float _baseDirY;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public FabrikSolver2 () {
            _segs = new FabrikSolver2Segment[8];
            Clear ();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Clear () {
            _basePosX = 0f;
            _basePosY = 0f;
            _segsLen = 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public FabrikSolver2 SetBase (float posX, float posY, float dirX, float dirY) {
            _basePosX = posX;
            _basePosY = posY;
            _baseDirX = dirX;
            _baseDirY = dirY;
            return this;
        }

        public int Add (float x, float y, float negAngle, float posAngle) {
            if (_segs.Length == _segsLen) {
                Array.Resize (ref _segs, _segsLen << 1);
            }
            float diffX;
            float diffY;
            if (_segsLen > 0) {
                ref var seg = ref _segs[_segsLen - 1];
                diffX = x - seg.EndX;
                diffY = y - seg.EndY;
            } else {
                diffX = x - _basePosX;
                diffY = y - _basePosY;
            }
            _segs[_segsLen++] = new () {
                EndX = x, EndY = y,
                Len = (float) Math.Sqrt (diffX * diffX + diffY * diffY),
                NegAngle = negAngle,
                NegAngleCos = (float) Math.Cos (negAngle),
                NegAngleSin = (float) Math.Sin (negAngle),
                PosAngle = posAngle,
                PosAngleCos = (float) Math.Cos (posAngle),
                PosAngleSin = (float) Math.Sin (posAngle)
            };
            return _segsLen - 1;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public (float x, float y) Get (int idx) {
            ref var seg = ref _segs[idx];
            return (seg.EndX, seg.EndY);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int Len () => _segsLen;

        public void Solve (float targetX, float targetY, bool limitAngles) {
            if (_segsLen < 2) {
                return;
            }
            float diffX;
            float diffY;
            float invLen;
            float signedAngle;
            // выравнивание к цели.
            ref var currSeg = ref _segs[_segsLen - 1];
            ref var prevSeg = ref _segs[_segsLen - 2];
            currSeg.EndX = targetX;
            currSeg.EndY = targetY;
            for (var i = _segsLen - 1; i > 0; i--) {
                currSeg = ref _segs[i];
                prevSeg = ref _segs[i - 1];
                diffX = prevSeg.EndX - currSeg.EndX;
                diffY = prevSeg.EndY - currSeg.EndY;
                invLen = currSeg.Len / (float) Math.Sqrt (diffX * diffX + diffY * diffY);
                prevSeg.EndX = diffX * invLen + currSeg.EndX;
                prevSeg.EndY = diffY * invLen + currSeg.EndY;
            }
            // выравнивание к базе.
            currSeg = ref _segs[0];
            diffX = currSeg.EndX - _basePosX;
            diffY = currSeg.EndY - _basePosY;
            invLen = 1f / (float) Math.Sqrt (diffX * diffX + diffY * diffY);
            diffX *= invLen;
            diffY *= invLen;
            // ограничение углов для первого сегмента.
            if (limitAngles) {
                signedAngle =
                    (float) Math.Acos (_baseDirX * diffX + _baseDirY * diffY) *
                    (_baseDirX * diffY - _baseDirY * diffX > 0f ? 1f : -1f);
                if (signedAngle < currSeg.NegAngle) {
                    // предельное вращение на negAngle.
                    diffX = currSeg.NegAngleCos * _baseDirX - currSeg.NegAngleSin * _baseDirY;
                    diffY = currSeg.NegAngleSin * _baseDirX + currSeg.NegAngleCos * _baseDirY;
                } else {
                    if (signedAngle > currSeg.PosAngle) {
                        // предельное вращение на posAngle.
                        diffX = currSeg.PosAngleCos * _baseDirX - currSeg.PosAngleSin * _baseDirY;
                        diffY = currSeg.PosAngleSin * _baseDirX + currSeg.PosAngleCos * _baseDirY;
                    }
                }
            }
            currSeg.EndX = diffX * currSeg.Len + _basePosX;
            currSeg.EndY = diffY * currSeg.Len + _basePosY;
            var prevDirX = diffX;
            var prevDirY = diffY;
            for (var i = 1; i < _segsLen; i++) {
                currSeg = ref _segs[i];
                prevSeg = ref _segs[i - 1];
                diffX = currSeg.EndX - prevSeg.EndX;
                diffY = currSeg.EndY - prevSeg.EndY;
                invLen = 1f / (float) Math.Sqrt (diffX * diffX + diffY * diffY);
                diffX *= invLen;
                diffY *= invLen;
                // ограничение углов.
                if (limitAngles) {
                    signedAngle =
                    (float) Math.Acos (prevDirX * diffX + prevDirY * diffY) *
                    (prevDirX * diffY - prevDirY * diffX > 0f ? 1f : -1f);
                    if (signedAngle < currSeg.NegAngle) {
                        // предельное вращение на negAngle.
                        diffX = currSeg.NegAngleCos * prevDirX - currSeg.NegAngleSin * prevDirY;
                        diffY = currSeg.NegAngleSin * prevDirX + currSeg.NegAngleCos * prevDirY;
                    } else {
                        if (signedAngle > currSeg.PosAngle) {
                            // предельное вращение на posAngle.
                            diffX = currSeg.PosAngleCos * prevDirX - currSeg.PosAngleSin * prevDirY;
                            diffY = currSeg.PosAngleSin * prevDirX + currSeg.PosAngleCos * prevDirY;
                        }
                    }
                }
                currSeg.EndX = diffX * currSeg.Len + prevSeg.EndX;
                currSeg.EndY = diffY * currSeg.Len + prevSeg.EndY;
                prevDirX = diffX;
                prevDirY = diffY;
            }
        }
    }
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    struct FabrikSolver2Segment {
        public float EndX;
        public float EndY;
        public float Len;

        public float NegAngle;
        public float NegAngleCos;
        public float NegAngleSin;
        public float PosAngle;
        public float PosAngleCos;
        public float PosAngleSin;
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
