using SFML.Graphics;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core.Rendering
{
    public class Renderer
    {
        private readonly RenderWindow window;

        public Color ClearColor { get; set; } = Color.Black;

        public Renderer(Screen screen)
        {
            window = screen.Window;
        }

        public void Render()
        {
            window.Clear(ClearColor);

            window.Display();
        }
    }
}
