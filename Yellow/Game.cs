using SFML.Graphics;
using Yellow.Assets;
using Yellow.Core;

namespace Yellow
{
    public class Game
    {
        public IAssetManager Assets { get; private set; } = new AssetManager();

        public Game()
        {
            Locator.Provide(this);
        }

        public Sprite MakeSprite(string name)
        {
            return Assets.MakeSprite(name);
        }
    }
}
