using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Sources.Frameworks.MyLeoEcsProto.Features
{
    public abstract class EcsFeature : IEcsFeature
    {
        private readonly IFeatureService _featureService;
        private readonly List<IProtoSystem> _systems = new ();
        private readonly List<IProtoInitSystem> _initSystems = new ();
        private readonly List<IProtoRunSystem> _runSystems = new ();
        private readonly List<IProtoDestroySystem> _destroySystems = new ();
        
        private bool _enabled = true;

        protected EcsFeature(IFeatureService featureService)
        {
            _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
        }

        public void Enable() =>
            _enabled = true;

        public void Disable() =>
            _enabled = false;

        public void Init(IProtoSystems systems)
        {
            _featureService.Add(this);
            Register();
            Inject(systems);
            
            foreach (IProtoInitSystem system in _initSystems)
                system.Init(systems);
        }

        public void Run()
        {
            if (_enabled == false)
                return;

            foreach (IProtoRunSystem system in _runSystems)
                system.Run();
        }

        public void Destroy()
        {
            foreach (IProtoDestroySystem system in _destroySystems)
                system.Destroy();
        }

        protected abstract void Register();
        
        protected EcsFeature AddSystem(IProtoSystem system)
        {
            if (system is IProtoInitSystem initSystem)
                _initSystems.Add(initSystem);

            if (system is IProtoRunSystem runSystem)
                _runSystems.Add(runSystem);

            if (system is IProtoDestroySystem destroySystem)
                _destroySystems.Add(destroySystem);
            
            _systems.Add(system);

            return this;
        }

        private void Inject(IProtoSystems systems)
        {
            Dictionary<Type, object> services = systems.Services();
            
            foreach (IProtoSystem system in _systems)
                AutoInjectModule.Inject(system, systems, services);
        }
    }
}