using Sources.Frameworks.GameServices.EntityPools.Interfaces.Bakers.Generic;
using TeoGames.Mesh_Combiner.Scripts.Combine;
using UnityEngine;

namespace Sources.Frameworks.GameServices.EntityPools.Implementation.Bakers
{
    public class PoolBaker<T> : IPoolBaker<T>
        where T : struct
    {
        private MeshCombiner _meshCombiner;
        private MeshRenderer _meshRenderer;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private bool _isInitialize;

        public PoolBaker(Transform parent)
        {
            _meshCombiner = new GameObject($"MeshCombiner of {typeof(T).Name}")
                .AddComponent<MeshCombiner>();
            _meshCombiner.bakeMaterials = true;
            _meshCombiner.transform.SetParent(parent);
        }

        public void Add(Transform transform)
        {
            transform.SetParent(_meshCombiner.transform);
            InitializeMeshRenderer();
        }

        private void InitializeMeshRenderer()
        {
            if (_meshRenderer != null)
                return;
            
            _meshRenderer = _meshCombiner.GetComponentInChildren<MeshRenderer>();
            _skinnedMeshRenderer = _meshCombiner.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }
}