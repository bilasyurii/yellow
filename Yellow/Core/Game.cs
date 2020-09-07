using Yellow.Assets.Abstractions;
using SFML.Graphics;
using Yellow.Core.ECS;
using Yellow.Core.Time;

namespace Yellow.Core
{
    public class Game
    {
        public World World { get; private set; }

        public IAssetManager Assets { get; private set; } = Locator.Get<IAssetManager>();

        public TimeManager Time { get; private set; }

        public Game(int entitiesPoolInitialSize = 100)
        {
            Time = new TimeManager(this);
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

        public void Update()
        {
            Time.Update();
        }
    }
}
