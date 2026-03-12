using Dott;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.HealthBoosters.Presentation
{
    //TODO все модули которые для юая добавить в название UI
    public class HealthBusterModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Button Button { get; private set; }
        [field: Required] [field: SerializeField] public TMP_Text Text { get; private set; }

        private IEntityRepository _repository;
        
        [Inject]
        private void Construct(IEntityRepository repository)
        {
            _repository = repository;
        }

        private void OnEnable() =>
            Button?.onClick.AddListener(OnClick);

        private void OnDisable() =>
            Button?.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            ProtoEntity healthBuster = _repository.GetByName(IdsConst.HealthBooster);

            if (healthBuster.GetHealthBuster().Value <= 0)
                return;
            
            healthBuster.AddDecreaseEvent();
        }
    }
}