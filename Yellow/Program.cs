using SFML.Graphics;
using SFML.Window;
using System.IO;
using Yellow.Assets.JSON;

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

            var game = new Game();

            game.Assets.LoadTexture("robot", @"img\sprite.png");

            var stream = new FileStream(@"..\..\assets\atlas\atlas01.json", FileMode.Open);

            using (StreamReader reader = new StreamReader(stream))
            {
                var atlasData = reader.ReadToEnd();
                var parser = new JParser();
                var json = parser.Parse(atlasData);
                System.Console.WriteLine(json["frames"]["tile"]["frame"]["w"].Integer);
            }

            //var sprite = new Sprite(game.Assets.GetTexture("robot"));

            var sprite = game.MakeSprite("robot");

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
