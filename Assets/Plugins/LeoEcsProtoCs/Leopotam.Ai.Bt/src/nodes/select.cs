// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtSelect<T> : IBtNode<T> where T : class {
        (IBtNode<T> cond, IBtNode<T> body)[] _pairs;
        int _idx;
        bool _inBody;

        public BtSelect (params (IBtNode<T> cond, IBtNode<T> body)[] pairs) {
            _pairs = pairs;
            _idx = 0;
            _inBody = false;
        }

        public void Reset () {
            _idx = 0;
            _inBody = false;
            foreach (var pair in _pairs) {
                pair.cond.Reset ();
                pair.body.Reset ();
            }
        }

        public BtResult Run (T data) {
            var res = BtResult.True;
            if (!_inBody) {
                while (_idx < _pairs.Length) {
                    res = _pairs[_idx].cond.Run (data);
                    if (res == BtResult.True) {
                        _inBody = true;
                        break;
                    }
                    if (res == BtResult.Pending || res == BtResult.Error) {
                        return res;
                    }
                    _idx++;
                }
                if (!_inBody) {
                    res = BtResult.True;
                }
            }
            if (_inBody) {
                res = _pairs[_idx].body.Run (data);
                if (res == BtResult.Pending || res == BtResult.Error) {
                    return res;
                }
                _idx = 0;
                _inBody = false;
                res = BtResult.True;
            }
            return res;
        }
    }
}
