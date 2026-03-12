using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime
{
    public class EntityLink : MonoBehaviour
    {
        [Header("Entity")] 
        [SerializeField] private List<EntityModule> _modules;
        public ProtoEntity Entity { get; private set; }
        public ProtoWorld World { get; private set; }

        public void Initialize(ProtoEntity entity, ProtoWorld world)
        {
            Entity = entity;
            World = world;
            
            foreach (EntityModule module in _modules)
                module.Initialize(entity, world, this);
        }
        
        public T GetModule<T>()
            where T : EntityModule
        {
            foreach (EntityModule module in _modules)
            {
                if (module is T concrete)
                    return concrete;
            }
            
            throw new InvalidOperationException("Module not found");
        }
        
        public bool TryGetModule<T>(out T findedModule)
            where T : EntityModule
        {
            foreach (EntityModule module in _modules)
            {
                if (module is T concrete)
                {
                    findedModule = concrete;
                    return true;
                }
            }

            findedModule = null;
            
            return false;
        }

        public IReadOnlyList<EntityModule> GetModules() =>
            _modules;

        [Button]
        private void FillModules()
        {
            EntityModule[] modules = gameObject.GetComponents<EntityModule>();
            AddModules(modules);
            modules = gameObject.GetComponentsInChildren<EntityModule>();
            AddModules(modules);
        }

        private void AddModules(EntityModule[] modules)
        {
            foreach (EntityModule module in modules)
            {
                if (_modules.Contains(module))
                    continue;
                
                _modules.Add(module);
            }
        }

        public EntityLink Show()
        {
            gameObject.SetActive(true);
            return this;
        }

        public EntityLink Hide()
        {
            gameObject.SetActive(false);
            return this;
        }
    }
}