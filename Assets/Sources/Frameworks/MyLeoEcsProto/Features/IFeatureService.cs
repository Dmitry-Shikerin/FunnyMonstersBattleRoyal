namespace Sources.Frameworks.MyLeoEcsProto.Features
{
    public interface IFeatureService
    {
        void Enable<T>() 
            where T : IEcsFeature;
        void Disable<T>()
            where T : IEcsFeature;
        void Add<T>(T feature)
            where T : IEcsFeature;
    }
}