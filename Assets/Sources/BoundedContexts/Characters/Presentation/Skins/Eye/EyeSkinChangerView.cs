using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation.Skins;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Eye
{
    public class EyeSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [Required] [SerializeField] private List<EyeSkinView> _skinViews;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        
        public EyeSkinName CurrentSkinName { get; private set; } = EyeSkinName.Eye01;
        
        private EyeSkinChangerUiView _view;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(EyeSkinChangerUiView view)
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
            
            if (CurrentIndex >= Enum.GetValues(typeof(GloveSkinName)).Length)
                CurrentIndex = 1; // Зацикливаем
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetPreviousSkin_Rpc()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(GloveSkinName)).Length - 1;// Зацикливаем
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (EyeSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(EyeSkinName nextSkinName)
        {
            if (nextSkinName == EyeSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (EyeSkinView view in _skinViews)
            {
                if (view.Name == EyeSkinName.Default)
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
            _skinViews = GetComponentsInChildren<EyeSkinView>(true).ToList();
        }
    }
}