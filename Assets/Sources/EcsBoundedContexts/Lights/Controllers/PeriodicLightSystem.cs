using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class PeriodicLightSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
            LightComponent,
            PeriodicLightComponent,
            TransformComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                
            }
            
            // void OnEnable()
            // {
            //     m_Light = GetComponent<Light>();
            //     m_Offset = m_MaxTimeOffset * Random.value;
            // }
            //
            // void Update()
            // {
            //     m_Light.intensity = m_MinIntensity + (m_MaxIntensity - m_MinIntensity) * Mathf.Sin((m_Offset + Time.time)  * 2f * Mathf.PI * m_Period);
            // }
        }
    }
}