using Sources.Frameworks.YandexSdkFramework.Leaderboards.Domain.Models;
using TMPro;
using UnityEngine;

namespace Sources.Frameworks.YandexSdkFramework.Leaderboards.Presentations.Implementation.Views
{
    public class LeaderBoardElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        public void Construct(LeaderBoardPlayerData playerData)
        {
            _playerName.text = playerData.Name;
            _playerRank.text = playerData.Rank.ToString();
            _playerScore.text = playerData.Score.ToString();
        }
    }
}