using SFML.Graphics;
using System.Collections.Generic;
using Yellow.Core.Components;
using Yellow.Core.ECS;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core.Rendering
{
    public class Renderer : ECS.System
    {
        private readonly RenderWindow window;

        public Color ClearColor { get; set; } = Color.Black;

        [ComponentBag(typeof(Graphic))]
        public List<Component> Renderable { private get; set; }

        public Renderer(Screen screen)
        {
            window = screen.Window;
        }

        public void Render()
        {
            window.Clear(ClearColor);

            foreach (var renderable in Renderable)
            {
                window.Draw(((Graphic)renderable).drawable);
            }

            window.Display();
        }
    }
}
