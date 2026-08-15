using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Players.Presentation.Ui
{
    public class GameplayPlayerNameUiView : MonoBehaviour
    {
        [Required] [SerializeField] private TMP_Text _playerNameText;

        public void InitPlayerName(string text)
        {
            _playerNameText.text = text;
        }
    }
}