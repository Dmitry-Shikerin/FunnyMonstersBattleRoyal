using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.MounthandNoses
{
    public class MouthandNosesSkinChangerView : NetworkBehaviour, IViewComponent
    {
        [Required] [SerializeField] private List<MouthandNosesSkinView> _skinViews;
        
        private MouthandNosesSkinChangerUiView _view;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        public MouthandNosesSkinName CurrentSkinName { get; private set; } = MouthandNosesSkinName.Mouth01;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(MouthandNosesSkinChangerUiView view)
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
            
            if (CurrentIndex >= Enum.GetValues(typeof(MouthandNosesSkinName)).Length)
                CurrentIndex = 1; // Зацикливаем
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        public void SetPreviousSkin_Rpc()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(MouthandNosesSkinName)).Length - 1;// Зацикливаем
        }

        private void OnChangeSkinIndex()
        {
            CurrentSkinName = (MouthandNosesSkinName)CurrentIndex;
            SetSkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetSkin(MouthandNosesSkinName nextSkinName)
        {
            if (nextSkinName == MouthandNosesSkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (MouthandNosesSkinView view in _skinViews)
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
        
        [Button]
        private void Fill()
        {
            _skinViews.Clear();
            _skinViews = GetComponentsInChildren<MouthandNosesSkinView>(true).ToList();
        }
    }
}