using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.StateActions
{
    [Category("Custom/UI")]
    public class HideViewsAction : ActionTask
    {
        public List<UiViewId> Views = new ();
        
        protected override void OnExecute() =>
            DeepUiBrain.ViewManager.Hide(Views);
    }
}