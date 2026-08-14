using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation.Skins.Body
{
    public class BodySkinChangerView : NetworkBehaviour, IViewComponent
    {
        [SerializeField] private List<BodySkinView> _skinViews;
        
        [Networked]
        [OnChangedRender(nameof(OnChangeSkinIndex))]
        public int CurrentIndex { get; set; }
        
        public BodySkinName CurrentSkinName { get; private set; } = BodySkinName.Body01;
        
        private BodySkinChangerUiView _view;
        public PlayerRef PlayerRef { get; private set; }

        public void Construct(BodySkinChangerUiView view) =>
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
            
            DecreaseSkinIndex();
        }

        public void SetNextSkin()
        {
            if (Runner.IsClient)
            {
                SetNextSkin_Rpc();
                return;
            }
            
            IncreaseSkinIndex();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetNextSkin_Rpc() =>
            IncreaseSkinIndex();

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetPreviousSkin_Rpc() =>
            DecreaseSkinIndex();

        private void IncreaseSkinIndex()
        {
            CurrentIndex++;
            
            if (CurrentIndex >= Enum.GetValues(typeof(BodySkinName)).Length)
                CurrentIndex = 1;
        }

        private void DecreaseSkinIndex()
        {
            CurrentIndex--;
            
            if (CurrentIndex <= 0)
                CurrentIndex = Enum.GetValues(typeof(BodySkinName)).Length - 1;
        }

        private void OnChangeSkinIndex()
        {
            Debug.Log($"On Change Skin");
            CurrentSkinName = (BodySkinName)CurrentIndex;
            SetBodySkin(CurrentSkinName);

            if (Runner.LocalPlayer != PlayerRef)
                return;
            
            _view.SetText(CurrentSkinName.ToString());
        }

        private void SetBodySkin(BodySkinName nextSkinName)
        {
            if (nextSkinName == BodySkinName.Default)
                throw new InvalidOperationException("Not correct skin name");
            
            foreach (BodySkinView view in _skinViews)
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

        [Button]
        private void Fill()
        {
            _skinViews.Clear();
            _skinViews = GetComponentsInChildren<BodySkinView>(true).ToList();
        }
    }
}