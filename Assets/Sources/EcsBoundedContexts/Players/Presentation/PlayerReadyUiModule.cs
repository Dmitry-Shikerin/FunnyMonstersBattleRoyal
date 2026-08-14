using System;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Players.Presentation
{
    public class PlayerReadyUiModule : EntityModule
    {
        [Required] [SerializeField] private TMP_Text _name;
        [Required] [SerializeField] private Image _image;

        private void Awake()
        {
            GetComponent<ProtoUnityAuthoring>();
        }

        public void HideInfo()
        {
            _name.text = String.Empty;
            _image.color = Color.clear;
        }
    }
}