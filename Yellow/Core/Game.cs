using Yellow.Assets.Abstractions;
using SFML.Graphics;

namespace Yellow.Core
{
    public class Game
    {
        public IAssetManager Assets { get; private set; } = Locator.Get<IAssetManager>();

        public Game()
        {
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
    }
}
