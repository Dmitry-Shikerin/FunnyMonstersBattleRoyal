using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.BoundedContexts.Settings.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;

namespace Sources.BoundedContexts.Hud.Controllers.Conditions
{
    [Category(NcCategoriesConst.Ui)]
    public class HasChangedSettings : ConditionTask
    {
        private SettingsView _settingsView;

        [Inject]
        private void Construct(IUiViewService uiViewService) =>
            _settingsView = uiViewService.Get<SettingsUiView>().SettingsView;

        protected override bool OnCheck() =>
            _settingsView.IsChanged;
    }
}