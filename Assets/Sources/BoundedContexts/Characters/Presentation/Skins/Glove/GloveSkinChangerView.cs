using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Glove
{
    public class GloveSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [Required] [SerializeField] private List<GloveSkinView> _skinViews;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        
        public GloveSkinName CurrentSkinName { get; private set; } = GloveSkinName.Glove01;
        
        private GloveSkinChangerUiView _view;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(GloveSkinChangerUiView view) =>
            _view = view;

        public void Init(PlayerRef playerRef) =>
            PlayerRef = playerRef;

        public void SetPreviousSkin()
        {
            if (Runner.IsClient)
            {
                SetPreviousSkin_Rpc();
                return;
            }
            
            DecreaseIndex();
        }       
        
        public void SetNextSkin()
        {
            if (Runner.IsClient)
            {
                SetNextSkin_Rpc();
                return;
            }
            
            IncreaseIndex();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetNextSkin_Rpc() =>
            IncreaseIndex();

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetPreviousSkin_Rpc() =>
            DecreaseIndex();

        private void IncreaseIndex()
        {
            CurrentIndex++;
            
            if (CurrentIndex >= Enum.GetValues(typeof(GloveSkinName)).Length)
                CurrentIndex = 1;
        }

        private void DecreaseIndex()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(GloveSkinName)).Length - 1;
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (GloveSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(GloveSkinName nextSkinName)
        {
            if (nextSkinName == GloveSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (GloveSkinView view in _skinViews)
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
        
        [Button]
        private void Fill()
        {
            _skinViews.Clear();
            _skinViews = GetComponentsInChildren<GloveSkinView>(true).ToList();
        }
    }
}