using System.Collections.Generic;
using NodeCanvas.StateMachines;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(UiManagerConfig), menuName = "Configs/" + nameof(UiManagerConfig), order = 51)]
    public class UiManagerConfig : ScriptableObject
    {
        [field: SerializeField] public FSM Fsm { get; private set; }
        [field: SerializeField] public Hud Hud { get; private set; }
        [field: SerializeField] public List<UiView> Views { get; private set; }
        [field: SerializeField] public List<UiPopUpView> PopUps { get; private set; }
    }
}