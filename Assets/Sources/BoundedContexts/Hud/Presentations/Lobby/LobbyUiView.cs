using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Characters.Presentation.Skins.Body;
using Sources.BoundedContexts.Characters.Presentation.Skins.BodyPart;
using Sources.BoundedContexts.Characters.Presentation.Skins.Eye;
using Sources.BoundedContexts.Characters.Presentation.Skins.Glove;
using Sources.BoundedContexts.Characters.Presentation.Skins.Head;
using Sources.BoundedContexts.Characters.Presentation.Skins.MounthandNoses;
using Sources.BoundedContexts.Characters.Presentation.Skins.Tail;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Lobby
{
    public class LobbyUiView : UiView
    {
        [field: Required] [field: SerializeField] public EntityLink PlayerNameLink { get; private set; }
        [field: Required] [field: SerializeField] public List<EntityLink> PlayersReadyUiLink { get; private set; }
        
        [field: Header("Skin Changers")]
        [field: Required] [field: SerializeField] public BodySkinChangerUiView BodySkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public BodyPartSkinChangerUiView BodyPartSkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public EyeSkinChangerUiView EyeSkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public GloveSkinChangerUiView GloveSkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public HeadSkinChangerUiView HeadSkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public MouthandNosesSkinChangerUiView MouthandNosesSkinChangerUiView { get; private set; }
        [field: Required] [field: SerializeField] public TailSkinChangerUiView TailSkinChangerUiView { get; private set; }
    }
}