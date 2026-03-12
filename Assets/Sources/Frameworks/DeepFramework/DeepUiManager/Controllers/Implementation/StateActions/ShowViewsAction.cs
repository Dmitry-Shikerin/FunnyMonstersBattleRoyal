using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.StateActions
{
    [Category("Custom/UI")]
    public class ShowViewsAction : ActionTask
    {
        public List<UiViewId> Views = new ();
        
        protected override void OnExecute() =>
            DeepUiBrain.ViewManager.Show(Views);
    }
}