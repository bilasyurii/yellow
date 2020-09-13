using Yellow.Core.Boot;
using Yellow.Core.ECS;
using Yellow.Core.InputManagement;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core
{
    public class Engine
    {
        public static void UseStartup<TStartup>() where TStartup : class, IStartup, new()
        {
            var startup = new TStartup();

            startup.ProvideServices();

            var game = Configure(startup);

            startup.Game = game;

            startup.PreloadAssets();

            startup.Prepare();

            game.Start();

            startup.OnGameEnded();
        }

        private static Game Configure(IStartup startup)
        {
            var configuration = new Configuration();

            startup.Configure(configuration);

            var screen = new Screen(configuration.Screen);
            var world = new World(configuration.World);
            var input = new Input(configuration.Input, screen);
            var game = new Game(world, input, screen);

            return game;
        }
    }
}
