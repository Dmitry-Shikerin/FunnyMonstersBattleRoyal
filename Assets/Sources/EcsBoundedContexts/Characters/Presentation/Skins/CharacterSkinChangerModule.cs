using System;
using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class CharacterSkinChangerModule : EntityModule
    {
        [Required] [SerializeField] private List<HeadSkinView> _headSkinViews;

        public void SetHeadSkin(HeadSkinName nextSkinName)
        {
            if (nextSkinName == HeadSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (HeadSkinView view in _headSkinViews)
            {
                if (view.Name == HeadSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in headSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        }
    }
}