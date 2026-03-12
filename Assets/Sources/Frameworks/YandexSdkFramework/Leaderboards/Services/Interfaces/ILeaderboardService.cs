namespace Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces
{
    public interface ILeaderboardService
    {
        void Initialize();
        void Destroy();
        void Fill();
    }
}