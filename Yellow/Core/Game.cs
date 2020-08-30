using Yellow.Assets.Abstractions;
using SFML.Graphics;
using Yellow.Core.ECS;

namespace Yellow.Core
{
    public class Game
    {
        public World World { get; private set; }

        public IAssetManager Assets { get; private set; } = Locator.Get<IAssetManager>();

        public Game(int entitiesPoolInitialSize = 100)
        {
            World = new World(entitiesPoolInitialSize);

            Locator.Provide(this);
        }

        public Sprite MakeSprite(string name)
        {
            return Assets.MakeSprite(name);
        }

        public Sprite MakeSprite(string name, string atlas)
        {
            return Assets.MakeSprite(name, atlas);
        }

        public Entity MakeEntity()
        {
            return World.CreateEntity();
        }
    }
}
