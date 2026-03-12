using System;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.YandexSdkFramework.Sdk.Domain;

namespace Sources.Frameworks.YandexSdkFramework.Sdk.Services
{
    public interface ISdkService
    {
        bool IsInAdv { get; }
        AddType LustAddType { get; }

        void Initialize();
        void Destroy();
        bool IsAccountAuthorized();
        void AuthorizePlayerAccount();
        void GameReady();
        void ShowSticky();
        void SetPlayerScore(int score);
        void ShowRewardedAdv();
        void ShowInterstitial();
    }
}