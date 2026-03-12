using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.Frameworks.MyLeoEcsProto.Factories
{
    public abstract class EntityFactory
    {
        private readonly IEntityRepository _repository;
        private readonly DiContainer _container;
        private readonly ProtoWorld _world;

        protected EntityFactory(
            IEntityRepository repository,
            ProtoWorld world, 
            GameAspect aspect,
            DiContainer container)
        {
            _repository = repository;
            _container = container;
            _world = world ?? throw new ArgumentNullException(nameof(world));
            Aspect = aspect ?? throw new ArgumentNullException(nameof(aspect));
        }

        protected GameAspect Aspect { get; }

        public abstract ProtoEntity Create(EntityLink link);

        protected void Authoring(EntityLink link, ProtoEntity entity)
        {
            if (link == null)
                throw new ArgumentNullException(nameof(link));
            
            _repository.AddBayHash(entity, link.gameObject);

            if (link.TryGetModule(out ProtoUnityAuthoring authoringModule))
                authoringModule.ProcessAuthoringForEntity(_world, entity);
            
            link.Initialize(entity, _world);
            entity.AddEntityLink(link, entity.GetHashCode(), entity);

            foreach (EntityModule module in link.GetModules())
                _container.Inject(module);
        }
        
        public void InitLink(EntityLink link, ProtoEntity entity, bool addByHash = true)
        {
            if (addByHash)
                _repository.AddBayHash(entity, link.gameObject);
            
            ProtoUnityAuthoring authoringModule = link.GetModule<ProtoUnityAuthoring>();

            if (authoringModule != null)
                authoringModule.ProcessAuthoringForEntity(_world, entity);
            
            link.Initialize(entity, _world);
            //entity.AddEntityLink(link, entity.GetHashCode());

            foreach (EntityModule module in link.GetModules())
            {
                _container.Inject(module);
            }
        }
    }
}