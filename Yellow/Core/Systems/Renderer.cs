using SFML.Graphics;
using Yellow.Core.ECS;
using Yellow.Core.ScreenManagement;

namespace Yellow.Core.Systems
{
    public class Renderer : ECS.System
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

            var states = RenderStates.Default;

            Render(World.root, states, false);

            window.Display();
        }

        private void Render(Entity entity, RenderStates states, bool dirty)
        {
            var transform = entity.Transform;

            if (dirty |= transform.Dirty)
            {
                transform.ForceUpdateTransform();
            }

            var graphic = entity.Graphic;

            if (graphic != null)
            {
                states.Transform = transform;
                window.Draw(graphic.drawable, states);
            }

            foreach (var child in transform)
            {
                Render(child.owner, states, dirty);
            }
        }
    }
}
