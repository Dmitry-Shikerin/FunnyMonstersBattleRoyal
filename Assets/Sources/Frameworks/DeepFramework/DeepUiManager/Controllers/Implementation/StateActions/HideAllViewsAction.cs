using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.StateActions
{
    [Category("Custom/UI")]
    public class HideAllViewsAction : ActionTask
    {
        protected override void OnExecute() =>
            DeepUiBrain.ViewManager.HideAll();
    }
}