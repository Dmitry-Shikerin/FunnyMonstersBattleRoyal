using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class GameplayUiView : UiView
    {
        [field: Required] [field: SerializeField] public EntityLink Bunker { get; private set; }         
        [field: Required] [field: SerializeField] public EntityLink Wallet { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink EnemySpawner { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink FlamethrowerAbilityApplier { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink CharacterSpawnerAbilityApplier { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink NukeAbilityApplier { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink HealthBuster { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink AttackUpgrade { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink HealthUpgrade { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink NukeAbilityUpgrade { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink FlamethrowerAbilityUpgrade { get; private set; }
    }
}