using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class FlickeringLightSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
            LightComponent,
            FlickeringLightComponent,
            TransformComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                
            }
            
            // IEnumerator FlickerCoroutine()
            // {
            //     while (true) {
            //         m_TargetIntensity = RandomUtils.Float(m_MinIntensity, m_MaxIntensity);
            //         yield return new WaitForSeconds(RandomUtils.Float(m_MinFlickeringTime, m_MaxFlickeringTime));
            //     }
            // }
            //
            // void Update()
            // {
            //     m_Light.intensity = Mathf.Lerp(m_Light.intensity, m_TargetIntensity, Time.deltaTime / m_IntensitySmoothing);
            // }
        }
    }
}