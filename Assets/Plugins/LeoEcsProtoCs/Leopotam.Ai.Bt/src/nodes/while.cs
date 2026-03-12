// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtWhile<T> : IBtNode<T> where T : class {
        IBtNode<T> _cond;
        IBtNode<T> _body;
        bool _inBody;

        public BtWhile (BtAct<T> cond, IBtNode<T> body) {
            _cond = cond;
            _body = body;
            _inBody = false;
        }

        public void Reset () {
            _inBody = false;
            _cond.Reset ();
            _body.Reset ();
        }

        public BtResult Run (T data) {
            while (true) {
                if (!_inBody) {
                    var resCond = _cond.Run (data);
                    switch (resCond) {
                        case BtResult.True:
                            _inBody = true;
                            break;
                        case BtResult.False:
                            return BtResult.True;
                        default:
                            return resCond;
                    }
                }
                var resBody = _body.Run (data);
                switch (resBody) {
                    case BtResult.True:
                        _inBody = false;
                        break;
                    case BtResult.False:
                        _inBody = false;
                        return BtResult.True;
                    default:
                        return resBody;
                }
            }
        }
    }
}
