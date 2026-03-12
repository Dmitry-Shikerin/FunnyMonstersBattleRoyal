using System;
using System.Collections.Generic;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Constant;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Domain.Constants;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Domain.Models;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Presentations.Implementation.Views;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using YG;
using YG.Utils.LB;

namespace Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Implementation
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        private readonly IUiViewService _uiViewService;
        private IReadOnlyList<LeaderBoardElementView> _leaderBoardElementViews;

        public YandexLeaderboardService(IUiViewService uiViewService)
        {
            _uiViewService = uiViewService;
        }

        public void Initialize()
        {
            YG2.onGetLeaderboard += OnGetLeaderboard;
            _leaderBoardElementViews = _uiViewService.Get<LeaderboardUiView>().LeaderboardElements;

            if (_leaderBoardElementViews == null)
                throw new NullReferenceException(nameof(_leaderBoardElementViews));

            foreach (LeaderBoardElementView view in _leaderBoardElementViews)
            {
                if (view != null)
                    continue;

                throw new NullReferenceException(nameof(_leaderBoardElementViews));
            }
        }

        public void Destroy()
        {
            YG2.onGetLeaderboard -= OnGetLeaderboard;
        }

        public void Fill()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (YG2.player.auth == false)
                return;

            YG2.GetLeaderboard(LeaderBoardConst.LeaderboardName);
        }

        private void OnGetLeaderboard(LBData data)
        {
            LBPlayerData[] players = data.players;
            
            int count = players.Length < _leaderBoardElementViews.Count
                ? players.Length
                : _leaderBoardElementViews.Count;

            for (var i = 0; i < count; i++)
            {
                int rank = players[i].rank;
                int score = players[i].score;
                string name = players[i].name;

                if (string.IsNullOrEmpty(name))
                    name = YG2.envir.language switch
                    {
                        LocalizationConst.English => LeaderBoardConst.EnglishAnonymous,
                        LocalizationConst.Turkish => LeaderBoardConst.TurkishAnonymous,
                        LocalizationConst.Russian => LeaderBoardConst.RussianAnonymous,
                        _ => LeaderBoardConst.EnglishAnonymous,
                    };

                _leaderBoardElementViews[i].Construct(new LeaderBoardPlayerData(rank, name, score));
            }
        }
    }
}