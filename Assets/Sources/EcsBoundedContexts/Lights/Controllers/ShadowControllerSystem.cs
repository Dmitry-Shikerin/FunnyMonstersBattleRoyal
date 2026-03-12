using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain;
using Sources.EcsBoundedContexts.Lights.Domain.Configs;
using Sources.EcsBoundedContexts.Lights.Domain.Enums;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class ShadowControllerSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
                ShadowControllerComponent,
                LightComponent>());

        private readonly IAssetCollector _assetCollector;
        private ShadowManagerConfigCollector _config;

        public ShadowControllerSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }
        
        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<ShadowManagerConfigCollector>();
        }

        public void Run()
        {
        }

        public float GetShadowRange(ProtoEntity entity)
        {
            ShadowControllerComponent shadowControllerComponent = entity.GetShadowController();
            LightComponent lightComponent =entity.GetLight();
            
            if (shadowControllerComponent.m_DistanceMode == DistanceMode.DEFAULT)
                return _config.CurrentConfig.m_DefaultMaxShadowDistance;
            
            if (shadowControllerComponent.m_DistanceMode == DistanceMode.PROPORTIONAL_TO_LIGHT_RANGE)
                return lightComponent.m_Range * shadowControllerComponent.m_LightRangeShadowDistanceCoeff;
            
            return shadowControllerComponent.m_CustomMaxShadowDistance;
        }

        public float GetIntensityReductionCoeff(ProtoEntity entity)
        {
            ShadowControllerComponent shadowControllerComponent = entity.GetShadowController();
            
            if (shadowControllerComponent.m_IntensityReductionMode == IntensityReductionMode.CUSTOM)
                return shadowControllerComponent.m_CustomIntensityReductionCoeff;
            
            return _config.CurrentConfig.m_DefaultIntensityReductionCoeff;
        }

        public float GetRangeReductionCoeff(ProtoEntity entity)
        {
            ShadowControllerComponent shadowControllerComponent = entity.GetShadowController();
            
            if (shadowControllerComponent.m_RangeReductionMode == RangeReductionMode.CUSTOM)
                return shadowControllerComponent.m_CustomRangeReductionCoeff;
            
            return _config.CurrentConfig.m_DefaultRangeReductionCoeff;
        }

        // public void ForceEnabled(bool enabled, ProtoEntity entity)
        // {
        //     ShadowControllerComponent shadowControllerComponent = entity.GetShadowController();
        //     LightComponent lightComponent = entity.GetLight();
        //     Light light = entity.GetLight().m_Light;
        //     
        //     if (lightComponent.m_RequestShadow == false)
        //         return;
        //
        //     StopAllCoroutines();
        //     m_IsShadowEnabled = enabled;
        //     
        //     if (m_IsShadowEnabled)
        //     {
        //         light.shadows = m_GameLight.m_ShadowMode;
        //         light.range = m_GameLight.m_Range;
        //         light.intensity = m_GameLight.m_Intensity;
        //         light.shadowStrength = m_GameLight.m_ShadowStrength;
        //     }
        //     else
        //     {
        //         light.shadows = LightShadows.None;
        //         light.shadowStrength = 0f;
        //
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightRange &&
        //             m_RangeReductionMode != RangeReductionMode.IGNORE)
        //         {
        //             light.range = m_GameLight.m_Range * GetRangeReductionCoeff();
        //         }
        //
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightIntensity &&
        //             m_IntensityReductionMode != IntensityReductionMode.IGNORE)
        //         {
        //             light.intensity = m_GameLight.m_Intensity * GetIntensityReductionCoeff();
        //         }
        //     }
        // }
        //
        // public void SetEnabled(bool enabled)
        // {
        //     if (m_GameLight == null)
        //         return;
        //
        //     if (!m_GameLight.m_RequestShadow)
        //         return;
        //
        //     if (enabled == m_IsShadowEnabled)
        //         return;
        //
        //     if (m_Light.enabled == false
        //         || this.enabled == false
        //         || gameObject.activeInHierarchy == false)
        //     {
        //         return;
        //     }
        //
        //     m_IsShadowEnabled = enabled;
        //
        //     StopAllCoroutines();
        //     if (m_IsShadowEnabled)
        //     {
        //         StartCoroutine(EnableLightShadowCoroutine());
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightRange &&
        //             m_RangeReductionMode != RangeReductionMode.IGNORE)
        //         {
        //             StartCoroutine(RestoreRangeCoroutine());
        //         }
        //
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightIntensity &&
        //             m_IntensityReductionMode != IntensityReductionMode.IGNORE)
        //         {
        //             StartCoroutine(RestoreIntensityCoroutine());
        //         }
        //     }
        //     else
        //     {
        //         StartCoroutine(DisableLightShadowCoroutine());
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightRange &&
        //             m_RangeReductionMode != RangeReductionMode.IGNORE)
        //         {
        //             StartCoroutine(ReduceRangeCoroutine());
        //         }
        //
        //         if (ShadowManager.m_Instance.m_Config.m_ReduceInactiveLightIntensity &&
        //             m_IntensityReductionMode != IntensityReductionMode.IGNORE)
        //         {
        //             StartCoroutine(ReduceIntensityCoroutine());
        //         }
        //     }
        // }
        //
        // public void SetResolutionQualityReduced(bool reduced)
        // {
        //     if (m_GameLight == null)
        //         return;
        //
        //     m_IsResolutionReduced = reduced;
        //
        //     int targetResolution;
        //     if (m_GameLight.m_ShadowResolution == UnityEngine.Rendering.LightShadowResolution.FromQualitySettings)
        //     {
        //         targetResolution = (int)QualitySettings.shadowResolution;
        //     }
        //     else
        //     {
        //         targetResolution = (int)m_GameLight.m_ShadowResolution;
        //     }
        //
        //     if (m_IsResolutionReduced)
        //     {
        //         // Reduce only if > LOW == 1
        //         targetResolution = (targetResolution > 1) ? targetResolution - 1 : 1;
        //     }
        //
        //     m_Light.shadowResolution = (UnityEngine.Rendering.LightShadowResolution)(targetResolution);
        // }
        //
        // IEnumerator DisableLightShadowCoroutine()
        // {
        //     while (m_Light.shadowStrength > 0f)
        //     {
        //         m_Light.shadowStrength =
        //             Mathf.Clamp(
        //                 m_Light.shadowStrength - Time.deltaTime / ShadowManager.m_Instance.m_FadeOutTime *
        //                 m_GameLight.m_ShadowStrength, 0, 1);
        //
        //         yield return null;
        //     }
        //
        //     m_Light.shadows = LightShadows.None;
        // }
        //
        // IEnumerator EnableLightShadowCoroutine()
        // {
        //     m_Light.shadows = m_GameLight.m_ShadowMode;
        //     while (m_Light.shadowStrength < m_GameLight.m_ShadowStrength)
        //     {
        //         m_Light.shadowStrength =
        //             Mathf.Clamp(
        //                 m_Light.shadowStrength + Time.deltaTime / ShadowManager.m_Instance.m_FadeInTime *
        //                 m_GameLight.m_ShadowStrength, 0, 1);
        //
        //         yield return null;
        //     }
        // }
        //
        //
        // IEnumerator ReduceRangeCoroutine()
        // {
        //     while (m_Light.range >= m_GameLight.m_Range * GetRangeReductionCoeff())
        //     {
        //         m_Light.range = m_Light.range -
        //                         Time.deltaTime / ShadowManager.m_Instance.m_RangeFadeInTime * m_GameLight.m_Range;
        //         yield return null;
        //     }
        // }
        //
        // IEnumerator RestoreRangeCoroutine()
        // {
        //     while (m_Light.range <= m_GameLight.m_Range)
        //     {
        //         m_Light.range = m_Light.range +
        //                         Time.deltaTime / ShadowManager.m_Instance.m_RangeFadeOutTime * m_GameLight.m_Range;
        //         yield return null;
        //     }
        // }
        //
        //
        // IEnumerator ReduceIntensityCoroutine()
        // {
        //     while (m_Light.intensity >= m_GameLight.m_Intensity * GetIntensityReductionCoeff())
        //     {
        //         m_Light.intensity = m_Light.intensity - Time.deltaTime /
        //             ShadowManager.m_Instance.m_IntensityFadeInTime * m_GameLight.m_Intensity;
        //         yield return null;
        //     }
        // }
        //
        // IEnumerator RestoreIntensityCoroutine()
        // {
        //     while (m_Light.intensity <= m_GameLight.m_Intensity)
        //     {
        //         m_Light.intensity = m_Light.intensity + Time.deltaTime /
        //             ShadowManager.m_Instance.m_IntensityFadeOutTime * m_GameLight.m_Intensity;
        //         yield return null;
        //     }
        // }
    }
}