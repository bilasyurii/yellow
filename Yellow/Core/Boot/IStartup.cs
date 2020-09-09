namespace Yellow.Core.Boot
{
    public interface IStartup
    {
        Game Game { get; set; }

        void ProvideServices();

        void Configure(Configuration configuration);

        void PreloadAssets();

        void Prepare();

        void OnGameEnded();
    }
}
