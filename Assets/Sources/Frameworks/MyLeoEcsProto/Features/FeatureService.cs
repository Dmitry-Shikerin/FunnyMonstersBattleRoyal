using System;
using System.Collections.Generic;

namespace Sources.Frameworks.MyLeoEcsProto.Features
{
    public class FeatureService : IFeatureService
    {
        private readonly Dictionary<Type, IEcsFeature> _features = new ();
        
        public void Enable<T>() where T : IEcsFeature
        {
            if (_features.TryGetValue(typeof(T), out IEcsFeature feature) == false)
                throw new KeyNotFoundException(nameof(T));
            
            feature.Enable();
        }

        public void Disable<T>() where T : IEcsFeature
        {
            if (_features.TryGetValue(typeof(T), out IEcsFeature feature) == false)
                throw new KeyNotFoundException(nameof(T));

            feature.Disable();
        }

        public void Add<T>(T feature) where T : IEcsFeature
        {
            if (_features.ContainsKey(typeof(T)))
                throw new InvalidOperationException(feature.GetType().Name);

            _features[feature.GetType()] = feature;
        }
    }
}