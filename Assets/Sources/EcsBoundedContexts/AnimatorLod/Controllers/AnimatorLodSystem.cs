using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Components;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Configs;
using Sources.EcsBoundedContexts.Animators;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.AnimatorLod.Controllers
{
    [EcsSystem(3)]
    [ComponentGroup(ComponentGroup.AnimatorLod)]
    [Aspect(AspectName.Game)]
    public class AnimatorLodSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                AnimatorComponent,
                AnimatorLodComponent,
                TransformComponent>());
        [DI] private readonly ProtoIt _cameraIt = new(
            It.Inc<
                MainCameraTag,
                CameraComponent>());
        private readonly IAssetCollector _assetCollector;

        private List<AnimatorLodSettingsConfig> _lods;
        private ProtoEntity _cameraEntity;

        public AnimatorLodSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _lods = _assetCollector.Get<AnimatorLodSettingsCollector>().Configs;
            _cameraEntity = _cameraIt.First().Entity;
            EnableAnimatorLOD();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                int frameCountdown = entity.GetAnimatorLod().FrameCountdown;
                
                if (frameCountdown <= 0)
                {
                    EnableAnimator(entity);
        
                    continue;
                }
        
                DisableAnimator(entity);
            }
        }

        private void EnableLODSystem(ProtoEntity entity, int maxValue = 5)
        {
            ref AnimatorLodComponent lodComponent = ref entity.GetAnimatorLod();
            lodComponent.FrameCountdown = Random.Range(0, maxValue);
        }
        
        private void DisableLODSystem(ProtoEntity entity)
        {
            Animator animator = entity.GetAnimator().Value; 
            animator.enabled = true;
            animator.speed = 1.0f;
        }
        
        private void EnableAnimator(ProtoEntity entity)
        {
            Animator animator = entity.GetAnimator().Value; 
            ref AnimatorLodComponent lodComponent = ref entity.GetAnimatorLod();

            int lodIndex = GetLodIndex(entity);
            int newFrameCount = _lods[lodIndex].FrameCount;
            SkinQuality skinQuality = _lods[lodIndex].MaxBoneWeight;
            int speed = newFrameCount + 1;
            
            if(animator.enabled == false)
                animator.enabled = true;
            
            if(Mathf.Approximately(animator.speed, speed) == false)
                animator.speed = speed;
        
            lodComponent.FrameCountdown = newFrameCount;
        
            SetMeshQuality(entity, skinQuality);
        }
        
        private void DisableAnimator(ProtoEntity entity)
        {
            Animator animator = entity.GetAnimator().Value;
            ref AnimatorLodComponent lodComponent = ref entity.GetAnimatorLod();
            
            if(animator.enabled)
                animator.enabled = false;
            
            lodComponent.FrameCountdown--;
        }
        
        private void SetMeshQuality(ProtoEntity entity, SkinQuality quality)
        {
            ref AnimatorLodComponent lodComponent = ref entity.GetAnimatorLod();
            SkinnedMeshRenderer[] renderers = lodComponent.SkinnedMeshRenderers;

            if (lodComponent.ChangeSkinWeights == false)
                return;
            
            if(renderers[0].quality == quality)
                return;
        
            foreach (SkinnedMeshRenderer renderer in renderers)
                renderer.quality = quality;
        }
        
        private void DisableAnimatorLOD()
        {
            foreach (var entity in _it)
                DisableLODSystem(entity);
        }
        
        private void EnableAnimatorLOD()
        {
            foreach (var entity in _it)
                EnableLODSystem(entity);
        }
        
        private int GetClosestLODFrameCount(Vector3 position, Vector3 cameraPosition, out SkinQuality quality)
        {
            float distanceToCamera = Vector3.Distance(position, cameraPosition);
        
            for (int i = 0; i < _lods.Count; i++)
            {
                if (distanceToCamera > _lods[i].Distance)
                {
                    quality = _lods[i].MaxBoneWeight;
                    return _lods[i].FrameCount;
                }
            }
        
            quality = SkinQuality.Auto;
            
            return 0;
        }
        
        private int GetLodIndex(ProtoEntity entity)
        {
            Vector3 position = entity.GetTransform().Value.position;
            Vector3 cameraPosition = _cameraEntity.GetTransform().Value.position;
            
            float distance = Vector3.Distance(position, cameraPosition);
            int low = 0;
            int high = _lods.Count - 1;
            int closestIndex = 0;
        
            while (low <= high)
            {
                int mid = (low + high) / 2;
                float midDistance = _lods[mid].Distance;
                
                if (Mathf.Approximately(midDistance, distance))
                {
                    closestIndex = mid;
                    break;
                }
                
                if (midDistance < distance)
                {
                    closestIndex = mid;
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
        
            return closestIndex;
        }
    }
}