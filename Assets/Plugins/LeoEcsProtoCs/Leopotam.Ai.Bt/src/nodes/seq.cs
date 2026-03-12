// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public class BtSeq<T> : IBtNode<T> where T : class {
        IBtNode<T>[] _nodes;
        int _idx;

        public BtSeq (params IBtNode<T>[] nodes) {
            _nodes = nodes;
            _idx = 0;
        }

        public void Reset () {
            _idx = 0;
            foreach (var n in _nodes) {
                n.Reset ();
            }
        }

        public BtResult Run (T data) {
            var res = BtResult.True;
            while (_idx < _nodes.Length) {
                res = _nodes[_idx].Run (data);
                if (res != BtResult.True) {
                    break;
                }
                _idx++;
            }
            if (res == BtResult.True || res == BtResult.False) {
                _idx = 0;
                res = BtResult.True;
            }
            return res;
        }
    }
}
