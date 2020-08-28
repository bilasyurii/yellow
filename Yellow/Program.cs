using SFML.Graphics;
using SFML.Window;
using System.IO;
using Yellow.Assets.Abstractions;
using Yellow.Assets.Atlases;
using Yellow.Assets.JSON;
using Yellow.Core;

namespace Yellow
{
    class Program
    {
        static void Main()
        {
            var videoMode = new VideoMode(960, 640);
            var window = new RenderWindow(videoMode, "Yellow");

            window.Closed += (sender, args) => window.Close();

            var clearColor = new Color(50, 50, 50);

            var cs = new CircleShape(100.0f)
            {
                FillColor = Color.Yellow
            };

            Locator.ProvideStandardServices();
            Locator.Provide<IAtlasParser>(new AtlasParser());
            Locator.Provide<IJsonParser>(new JsonParser());

            var game = new Game();

            game.Assets.LoadTexture("robot", @"img\spritelist.png");
            game.Assets.LoadAtlas("robotAtlas", @"atlas\atlas01.json", game.Assets.GetTexture("robot"));

            //var sprite = new Sprite(game.Assets.GetTexture("robot"));

            var sprite = game.MakeSprite("tile");

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(clearColor);
                window.Draw(cs);

                window.Draw(sprite);

                window.Display();
            }
        }
    }
}
