using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Characters.Presentation.Skins.MounthandNoses;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation.Skins;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Tail
{
    public class TailSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [Required] [SerializeField] private List<TailSkinView> _skinViews;
        
        private TailSkinChangerUiView _view;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        public TailSkinName CurrentSkinName { get; private set; } = TailSkinName.Tail01;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(TailSkinChangerUiView view)
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
            
            if (CurrentIndex >= Enum.GetValues(typeof(TailSkinName)).Length)
                CurrentIndex = 1; // Зацикливаем
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetPreviousSkin_Rpc()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(TailSkinName)).Length - 1;// Зацикливаем
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (TailSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(TailSkinName nextSkinName)
        {
            if (nextSkinName == TailSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (TailSkinView view in _skinViews)
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
        
        [Button]
        private void Fill()
        {
            _skinViews.Clear();
            _skinViews = GetComponentsInChildren<TailSkinView>(true).ToList();
        }
    }
}