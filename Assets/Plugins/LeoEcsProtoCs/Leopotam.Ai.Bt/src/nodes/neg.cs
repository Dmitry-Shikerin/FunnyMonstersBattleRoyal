// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtNeg<T> : IBtNode<T> where T : class {
        IBtNode<T> _cond;

        public BtNeg (IBtNode<T> cond) => _cond = cond;

        public void Reset () {
            _cond.Reset ();
        }

        public BtResult Run (T data) {
            var res = _cond.Run (data);
            switch (res) {
                case BtResult.True:
                    return BtResult.False;
                case BtResult.False:
                    return BtResult.True;
                default:
                    return res;
            }
        }
    }
}
