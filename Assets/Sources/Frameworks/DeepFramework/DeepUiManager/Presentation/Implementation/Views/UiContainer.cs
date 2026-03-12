using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{

    public class UiContainer : UiContainerBase
    {
        //Const
        private const string UiContainerLabel = "<size=18><b><color=#0000FF><i>UiContainer</i></color></b></size>";
        
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _label = UiContainerLabel;
    }
}