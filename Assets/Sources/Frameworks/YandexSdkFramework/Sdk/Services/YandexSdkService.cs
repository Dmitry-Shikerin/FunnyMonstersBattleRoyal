using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Domain.Constants;
using Sources.Frameworks.YandexSdkFramework.Sdk.Domain;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace Sources.Frameworks.YandexSdkFramework.Sdk.Services
{
    public class YandexSdkService : ISdkService
    {
        private readonly IStorageService _storageService;
        private readonly ISoundService _soundService;
        private readonly IUiViewService _uiViewService;
        private readonly IEntityRepository _entityRepository;
        private readonly IPauseService _pauseService;
        private readonly TimeSpan _timeSpan = TimeSpan.FromSeconds(35);
        private CancellationTokenSource _token;

        private ProtoEntity _healthBuster;
        private int _score;
        private string _rewardID;
        private bool _isAvailable = true;

        public YandexSdkService(
            ISoundService soundService,
            IUiViewService uiViewService,
            IEntityRepository entityRepository,
            IStorageService storageService,
            IPauseService pauseService)
        {
            _soundService = soundService;
            _uiViewService = uiViewService;
            _entityRepository = entityRepository;
            _pauseService = pauseService;
            _storageService = storageService;
        }

        public AddType LustAddType { get; private set; }

        public bool IsInAdv { get; private set; }

        public void Initialize()
        {
            _token = new CancellationTokenSource();
            //_healthBuster = _entityRepository.GetByName(IdsConst.HealthBooster);
            YG2.onGetSDKData += OnGetSdkData;
            YG2.onGetLeaderboard += OnGetLeaderboard;
            YG2.onOpenRewardedAdv += OnOpenRewardAdv;
            YG2.onOpenInterAdv += OnOpenInterAdv;
            YG2.onCloseInterAdv += OnCloseInterAdv;
            YG2.onCloseInterAdv += StartTimer;
            YG2.onCloseRewardedAdv += OnCloseRewardAdv;
            YG2.onRewardAdv += OnReward;
        }

        public void Destroy()
        {
            YG2.onGetSDKData -= OnGetSdkData;
            YG2.onGetLeaderboard -= OnGetLeaderboard;
            YG2.onOpenRewardedAdv -= OnOpenRewardAdv;
            YG2.onOpenInterAdv -= OnOpenInterAdv;
            YG2.onCloseInterAdv -= OnCloseInterAdv;
            YG2.onCloseInterAdv -= StartTimer;
            YG2.onCloseRewardedAdv -= OnCloseRewardAdv;
            YG2.onRewardAdv -= OnReward;
            _token?.Cancel();
        }

        public void GameReady()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            YG2.GameReadyAPI();
        }

        public void ShowSticky()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            YG2.StickyAdActivity(true);
        }

        #region AccountAuth
        public bool IsAccountAuthorized()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return false;

            return YG2.player.auth;
        }

        public void AuthorizePlayerAccount()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (YG2.player.auth)
                return;
            
            YG2.OpenAuthDialog();
        }

        //Сработает после авторизации аккаунта
        private void OnGetSdkData()
        {
            _uiViewService.Show(UiViewId.MainHud);
        }
        #endregion

        #region Leaderboard
        public void SetPlayerScore(int score)
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (YG2.player.auth == false)
                return;
            
            _score = score;
            YG2.GetLeaderboard(LeaderBoardConst.LeaderboardName);
        }

        //Сработает после того как мы вызовем метод YG2.GetLeaderboard();
        private void OnGetLeaderboard(LBData data)
        {
            if (data.currentPlayer.score > _score)
                return;
                
            YG2.SetLeaderboard(LeaderBoardConst.LeaderboardName, _score);
        }
        #endregion
        
        #region Interstitial
        public void ShowInterstitial()
        {
            if (_isAvailable == false)
                return;
            
            YG2.InterstitialAdvShow();
            LustAddType = AddType.Inter;
        }

        //Сработает после того как откроется реклама;
        private void OnOpenRewardAdv()
        {
            _soundService.PauseSounds();
            _soundService.PauseMusic();
            IsInAdv = true;
        }  
        
        private void OnOpenInterAdv()
        {
            IsInAdv = true;
        }
        
        //Сработает после того как закроется реклама
        private void OnCloseRewardAdv()
        {
            _soundService.UnpauseMusic();
            IsInAdv = false;
        }      
        
        private void OnCloseInterAdv()
        {
            _soundService.UnpauseMusic();
            _uiViewService.Show(UiViewId.ContinueGameView);
            IsInAdv = false;
        }
        
        private async void StartTimer()
        {
            try
            {
                _isAvailable = false;
                await UniTask.Delay(_timeSpan, cancellationToken: _token.Token);
                _isAvailable = true;
            }
            catch (OperationCanceledException)
            {
            }
        }
        #endregion

        #region RewardAdd
        public void ShowRewardedAdv()
        {
            _rewardID = "AddCoins";
            YG2.RewardedAdvShow(_rewardID);
            LustAddType = AddType.Rewarded;
        }
        
        private void OnReward(string id)
        {
            if (id != _rewardID)
                return;
            
            //TODO вынести в конфиг
            //_healthBuster.AddIncreaseHealthBoosterEvent(5);
        }
        #endregion    
    }
}