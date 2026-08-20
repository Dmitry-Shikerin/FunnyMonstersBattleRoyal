using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Players.Presentation.Ui
{
    public class PlayerNameUiView : MonoBehaviour
    {
        [Required] [SerializeField] private TMP_Text _playerNameText;

        public void SetPlayerName(string text)
        {
            _playerNameText.text = text;
        }
    }
}