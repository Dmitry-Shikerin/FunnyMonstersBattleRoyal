using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation.Skins;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Head
{
    public class HeadSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [Required] [SerializeField] private List<HeadSkinView> _skinViews;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        
        public HeadSkinName CurrentSkinName { get; private set; } = HeadSkinName.Ear01;
        
        private HeadSkinChangerUiView _view;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(HeadSkinChangerUiView view)
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
            
            if (CurrentIndex >= Enum.GetValues(typeof(HeadSkinName)).Length)
                CurrentIndex = 1; // Зацикливаем
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetPreviousSkin_Rpc()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(HeadSkinName)).Length - 1;// Зацикливаем
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (HeadSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(HeadSkinName nextSkinName)
        {
            if (nextSkinName == HeadSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (HeadSkinView view in _skinViews)
            {
                if (view.Name == HeadSkinName.Default)
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
            _skinViews = GetComponentsInChildren<HeadSkinView>(true).ToList();
        }
    }
}