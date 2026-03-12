using System;
using System.Collections.Generic;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Infrastructure.Sequences
{
    public class SequencePool : IDisposable
    {
        private readonly Queue<DeepSequence> _pool = new ();
        
        public DeepSequence Get()
        {
            if (_pool.Count == 0)
                return Create();
            
            return _pool.Dequeue();
        }

        private DeepSequence Create()
        {
            DeepSequence sequence = new DeepSequence();
            sequence.SetPool(this);
            
            return sequence;
        }

        public void Return(DeepSequence sequence)
        {
            if (_pool.Contains(sequence))
                throw new InvalidOperationException();
            
            _pool.Enqueue(sequence);
        }

        public void Dispose()
        {
            _pool.Clear();
        }
    }
}