using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.StateActions
{
    [Category("Custom/UI")]
    public class HandleUiActionsAction : ActionTask
    {
        public List<UiActionId> Actions = new ();
        
        protected override void OnExecute()
        {
            DeepUiBrain.ActionHandler.Handle(Actions);
        }
    }
}