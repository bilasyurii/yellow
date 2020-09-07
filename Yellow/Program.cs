using SFML.Graphics;
using SFML.Window;
using Yellow.Assets.Abstractions;
using Yellow.Assets.Atlases;
using Yellow.Assets.JSON;
using Yellow.Core;
using Yellow.Core.Components;

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

            var sprite = game.MakeSprite("tile");

            var entity = game.MakeEntity();
            var transform = entity.Transform = new TransformComponent();
            var graphic = new Graphic(sprite);
            entity.Add(graphic);
            transform.Translate(100f, 100f);
            transform.SetPivot(35f, 19f);

            game.Time.Events.Repeat(1000, 1, (args) => System.Console.WriteLine("Hello, Yellow!"));

            game.Time.Events.Loop(500, (args) => System.Console.WriteLine("Hello!"));

            while (window.IsOpen)
            {
                game.Update();

                window.DispatchEvents();

                window.Clear(clearColor);

                window.Draw(cs);
                transform.Rotate(0.001f);
                transform.SetScale(transform.scaleX + 0.001f);
                transform.UpdateLocalTransform();
                var states = RenderStates.Default;
                states.Transform.Combine(transform.LocalTransform);
                window.Draw(sprite, states);

                window.Display();
            }
        }
    }
}
