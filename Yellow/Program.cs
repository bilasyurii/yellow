using SFML.Graphics;
using SFML.Window;

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

            CircleShape cs = new CircleShape(100.0f)
            {
                FillColor = Color.Yellow
            };

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(clearColor);
                window.Draw(cs);

                window.Display();
            }
        }
    }
}
