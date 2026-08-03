using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using TMPro;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Players.Presentation
{
    public class GameplayPlayerNameUiModule : EntityModule
    {
        [Required] [SerializeField] private TMP_Text _playerNameText;

        public void InitPlayerName(string text)
        {
            _playerNameText.text = text;
        }
        
        public void GeneratePlayerName()
        {
            string randomName = $"PlayerName.{Random.Range(0, 9999)}";
            InitPlayerName(randomName);
            Entity.ReplacePlayerName(randomName);
            Entity.AddSaveDataEvent();
        }
    }
}