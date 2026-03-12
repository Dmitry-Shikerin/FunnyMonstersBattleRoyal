// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtIf<T> : IBtNode<T> where T : class {
        IBtNode<T> _cond;
        IBtNode<T> _onSuccess;
        IBtNode<T> _onFail;
        bool _inBody;
        bool _condResult;

        public BtIf (BtAct<T> cond, IBtNode<T> onSuccess, IBtNode<T> onFail) {
            _cond = cond;
            _onSuccess = onSuccess;
            _onFail = onFail;
            _inBody = false;
            _condResult = false;
        }

        public void Reset () {
            _inBody = false;
            _condResult = false;
            _cond.Reset ();
            _onSuccess.Reset ();
            _onFail.Reset ();
        }

        public BtResult Run (T data) {
            BtResult res;
            if (!_inBody) {
                res = _cond.Run (data);
                switch (res) {
                    case BtResult.True:
                        _inBody = true;
                        _condResult = true;
                        break;
                    case BtResult.False:
                        _inBody = true;
                        _condResult = false;
                        break;
                    default:
                        return res;
                }
            }
            if (_condResult) {
                res = _onSuccess.Run (data);
            } else {
                res = _onFail.Run (data);
            }
            if (res == BtResult.True || res == BtResult.False) {
                _inBody = false;
                res = BtResult.True;
            }
            return res;
        }
    }
}
