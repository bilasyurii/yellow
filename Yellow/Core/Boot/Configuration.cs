using System;

namespace Yellow.Core.Boot
{
    public class Configuration
    {
        public ScreenBuilder Screen { get; } = new ScreenBuilder();

        public WorldBuilder World { get; } = new WorldBuilder();

        public InputBuilder Input { get; } = new InputBuilder();

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

        public Configuration ConfigureInput(Action<InputBuilder> configureInputAction)
        {
            configureInputAction(Input);

            return this;
        }
    }
}
