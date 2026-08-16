using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Players.Presentation
{
    public class PlayerReadyUiView : MonoBehaviour
    {
        [Required] [SerializeField] private TMP_Text _name;
        [Required] [SerializeField] private Image _image;

        public void SetName(string playerName)
        {
            _name.text = playerName;
        }

        public void SetReady(bool isReady)
        {
            if (isReady)
            {
                _image.color = Color.green;
                return;
            }
            
            _image.color = Color.red;
        }

        public void HideInfo()
        {
            _name.text = String.Empty;
            _image.color = Color.clear;
        }
    }
}