// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

namespace Leopotam.Ai.Bt {
    public enum BtResult { True, False, Pending, Error }

    public interface IBtNode<T> where T : class {
        void Reset ();
        BtResult Run (T data);
    }

    public delegate BtResult BtActHandler<T> (T data) where T : class;

    public struct BtNodes<T> where T : class { }

    public static partial class BtNodesExtensions {
        public static BtAct<T> Act<T> (this BtNodes<T> _, BtActHandler<T> cb) where T : class {
            return new (cb);
        }

        public static BtIf<T> If<T> (this BtNodes<T> _, BtAct<T> cond, IBtNode<T> onSuccess, IBtNode<T> onFail) where T : class {
            return new (cond, onSuccess, onFail);
        }

        public static BtNeg<T> Neg<T> (this BtNodes<T> _, IBtNode<T> node) where T : class {
            return new (node);
        }

        public static BtAnd<T> And<T> (this BtNodes<T> _, IBtNode<T> nodeA, IBtNode<T> nodeB) where T : class {
            return new (nodeA, nodeB);
        }

        public static BtOr<T> Or<T> (this BtNodes<T> _, IBtNode<T> nodeA, IBtNode<T> nodeB) where T : class {
            return new (nodeA, nodeB);
        }

        public static BtSelect<T> Select<T> (this BtNodes<T> _, params (IBtNode<T> cond, IBtNode<T> body)[] pairs) where T : class {
            return new (pairs);
        }

        public static BtSeq<T> Seq<T> (this BtNodes<T> _, params IBtNode<T>[] nodes) where T : class {
            return new (nodes);
        }

        public static BtWhile<T> While<T> (this BtNodes<T> _, BtAct<T> cond, IBtNode<T> body) where T : class {
            return new (cond, body);
        }
    }
}
