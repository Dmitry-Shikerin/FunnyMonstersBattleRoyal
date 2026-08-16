using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.BoundedContexts.Settings.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUtils.Managers;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Controllers.Conditions
{
    [Category(NcCategoriesConst.Ui)]
    public class ToWarningSaveSettingsCondition : ConditionTask
    {
        private IEntityRepository _repository;
        private SettingsView _settingsView;
        private ISignalBus _signalBus;

        [Inject]
        private void Construct(
            ISignalBus signalBus,
            IUiViewService uiViewService)
        {
            _signalBus = signalBus;
            _settingsView = uiViewService.Get<SettingsUiView>().SettingsView;
        }

        protected override void OnEnable()
        {
            _signalBus.Subscribe<KeyPressedSignal>(KeyPressed);
            _signalBus.Subscribe<OnClickSignal>(OnClick);
        }

        protected override void OnDisable()
        {
            if (DeepCoreManager.IsApplicationQuitting)
                return;
            
            _signalBus.Unsubscribe<KeyPressedSignal>(KeyPressed);
            _signalBus.Unsubscribe<OnClickSignal>(OnClick);
        }

        private void OnClick(OnClickSignal signal)
        {
            if (signal.ButtonId != ButtonId.Back)
                return;

            if (_settingsView.IsChanged == false)
                return;
            
            YieldReturn(true);
        }

        private void KeyPressed(KeyPressedSignal signal)
        {
            if (signal.Key != KeyCode.Escape)
                return;

            if (_settingsView.IsChanged == false)
                return;
            
            YieldReturn(true);
        }

        protected override bool OnCheck() =>
            false;
    }
}