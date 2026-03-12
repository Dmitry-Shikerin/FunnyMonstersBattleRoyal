using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class FireLightSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
                LightComponent, 
                FireLightComponent, 
                TransformComponent>());
        
        public void Run()
        {
            // foreach (ProtoEntity entity in _it)
            // {
            //     ref var lightComponent = ref entity.; 
            //     
            //     m_Light.intensity = Mathf.Lerp(m_Light.intensity, m_TargetIntensity, Time.deltaTime / m_IntensitySmoothing);
            //     transform.position = Vector3.Lerp(transform.position, m_TargetPos, Time.deltaTime / m_PositionSmoothing);
            //     
            // }
            //
            // //Corutine
            // m_TargetIntensity = RandomUtils.Float(m_MinIntensity, m_MaxIntensity);
            // m_TargetPos = m_DefaultPos + RandomUtils.Vector3(-m_MaxPositionOffset, m_MaxPositionOffset);
            //
            // yield return new WaitForSeconds(RandomUtils.Float(m_MinFlickeringTime, m_MaxFlickeringTime));
        }
    }
}