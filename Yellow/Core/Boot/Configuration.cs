using System;

namespace Yellow.Core.Boot
{
    public class Configuration
    {
        public ScreenBuilder Screen { get; private set; } = new ScreenBuilder();

        public WorldBuilder World { get; private set; } = new WorldBuilder();

        public Configuration ConfigureScreen(Action<ScreenBuilder> configureScreenAction)
        {
            configureScreenAction(Screen);

            return this;
        }

        public Configuration ConfigureWorld(Action<WorldBuilder> configureWorldAction)
        {
            configureWorldAction(World);

            return this;
        }
    }
}
