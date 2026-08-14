using System;
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
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Skins
{
    public class CharacterSkinChangerModule : EntityModule
    {
        [Required] [SerializeField] private List<HeadSkinView> _headSkinViews;
        [Required] [SerializeField] private List<EyeSkinView> _eyeSkinViews;
        [Required] [SerializeField] private List<BodyPartSkinView> _bodyPartSkinViews;
        [Required] [SerializeField] private List<BodySkinView> _bodySkinViews;
        [Required] [SerializeField] private List<GloveSkinView> _gloveSkinViews;
        [Required] [SerializeField] private List<MouthandNosesSkinView> _gloveMouthandNosesViews;
        [Required] [SerializeField] private List<TailSkinView> _tailSkinViews;

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
        
        public void SetEyeSkin(EyeSkinName nextSkinName)
        {
            if (nextSkinName == EyeSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (EyeSkinView view in _eyeSkinViews)
            {
                if (view.Name == EyeSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in eyeSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        } 
        
        public void SetBodyPartSkin(BodyPartSkinName nextSkinName)
        {
            if (nextSkinName == BodyPartSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (BodyPartSkinView view in _bodyPartSkinViews)
            {
                if (view.Name == BodyPartSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in BodyPartSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        }  
        
        public void SetBodySkin(BodySkinName nextSkinName)
        {
            if (nextSkinName == BodySkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (BodySkinView view in _bodySkinViews)
            {
                if (view.Name == BodySkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in BodyPartSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        }      
        
        public void SetGloveSkin(GloveSkinName nextSkinName)
        {
            if (nextSkinName == GloveSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (GloveSkinView view in _gloveSkinViews)
            {
                if (view.Name == GloveSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in BodyPartSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        }
        
        public void SetMouthandNosesSkin(MouthandNosesSkinName nextSkinName)
        {
            if (nextSkinName == MouthandNosesSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (MouthandNosesSkinView view in _gloveMouthandNosesViews)
            {
                if (view.Name == MouthandNosesSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in BodyPartSkinView");

                if (view.Name != nextSkinName)
                {
                    view.gameObject.SetActive(false);
                    continue;
                }
                
                view.gameObject.SetActive(true);
            }
        }
        
        public void SetTailSkin(TailSkinName nextSkinName)
        {
            if (nextSkinName == TailSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (TailSkinView view in _tailSkinViews)
            {
                if (view.Name == TailSkinName.Default)
                    throw new InvalidOperationException("Not correct skin name in BodyPartSkinView");

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