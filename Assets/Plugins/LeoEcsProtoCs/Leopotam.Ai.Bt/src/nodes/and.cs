// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtAnd<T> : IBtNode<T> where T : class {
        IBtNode<T> _a;
        IBtNode<T> _b;
        bool _inB;

        public BtAnd (IBtNode<T> a, IBtNode<T> b) {
            _a = a;
            _b = b;
            _inB = false;
        }

        public void Reset () {
            _inB = false;
            _a.Reset ();
            _b.Reset ();
        }

        public BtResult Run (T data) {
            BtResult res;
            if (!_inB) {
                res = _a.Run (data);
                if (res != BtResult.True) {
                    return res;
                }
                _inB = true;
            }
            res = _b.Run (data);
            if (res == BtResult.True || res == BtResult.False) {
                _inB = false;
            }
            return res;
        }
    }
}
