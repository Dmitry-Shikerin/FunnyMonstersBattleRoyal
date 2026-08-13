using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Controllers.Conditions
{
    [Category(NcCategoriesConst.Ui)]
    public class ToWarningSaveSettingsCondition : ConditionTask
    {
        private IEntityRepository _repository;
        private ProtoEntity _entity;

        [Inject]
        private void Construct(IEntityRepository repository) =>
            _repository = repository;

        protected override string OnInit()
        {
            _entity = _repository.GetByName(IdsConst.Settings);
            return null;
        }

        protected override void OnEnable()
        {
            DeepUiBrain.SignalBus.Subscribe<KeyPressedSignal>(KeyPressed);
            DeepUiBrain.SignalBus.Subscribe<OnClickSignal>(OnClick);
        }

        protected override void OnDisable()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            DeepUiBrain.SignalBus.Unsubscribe<KeyPressedSignal>(KeyPressed);
            DeepUiBrain.SignalBus.Unsubscribe<OnClickSignal>(OnClick);
        }

        private void OnClick(OnClickSignal signal)
        {
            if (signal.ButtonId != ButtonId.Back)
                return;

            if (_entity.HasChangedSettings() == false)
                return;
            
            YieldReturn(true);
        }

        private void KeyPressed(KeyPressedSignal signal)
        {
            if (signal.Key != KeyCode.Escape)
                return;

            if (_entity.HasChangedSettings() == false)
                return;
            
            YieldReturn(true);
        }

        protected override bool OnCheck() =>
            false;
    }
}