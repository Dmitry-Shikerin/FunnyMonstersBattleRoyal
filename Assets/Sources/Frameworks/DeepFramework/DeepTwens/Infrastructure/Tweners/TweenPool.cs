using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Tweners;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Tweners
{
    public class TweenPool : IDisposable
    {
        private readonly Queue<Tween> _pool = new ();
        
        public Tween Get()
        {
            if (_pool.Count == 0)
                return Create();
            
            Tween tweener = _pool.Dequeue();
            tweener.Reset();

            return tweener;
        }

        private Tween Create()
        {
            Tween sequence = new Tween();
            sequence.SetPool(this);
            
            return sequence;
        }

        public void Return(Tween sequence)
        {
            if (sequence == null)
                return;

            if (_pool == null)
                return;
            
            if (_pool.Contains(sequence))
                return;
            
            _pool.Enqueue(sequence);
        }

        public void Dispose()
        {
            foreach (Tween tweener in _pool)
            {
                tweener?.Dispose();
            }
        }
    }
}