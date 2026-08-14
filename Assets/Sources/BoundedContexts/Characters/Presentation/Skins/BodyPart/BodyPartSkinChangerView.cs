using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation.Skins;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.BodyPart
{
    public class BodyPartSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [SerializeField] private List<BodyPartSkinView> _skinViews;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        
        public BodyPartSkinName CurrentSkinName { get; private set; } = BodyPartSkinName.BodyPart01;
        
        private BodyPartSkinChangerUiView _view;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(BodyPartSkinChangerUiView view)
        {
            _view = view;
        }

        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetNextSkin_Rpc()
        {
            CurrentIndex++;
            
            if (CurrentIndex >= Enum.GetValues(typeof(BodyPartSkinName)).Length)
                CurrentIndex = 1; // Зацикливаем
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetPreviousSkin_Rpc()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(BodyPartSkinName)).Length - 1;// Зацикливаем
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (BodyPartSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(BodyPartSkinName nextSkinName)
        {
            if (nextSkinName == BodyPartSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (BodyPartSkinView view in _skinViews)
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
        
        [Button]
        private void Fill()
        {
            _skinViews.Clear();
            _skinViews = GetComponentsInChildren<BodyPartSkinView>(true).ToList();
        }
    }
}