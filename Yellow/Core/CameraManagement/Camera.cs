using SFML.Graphics;
using Yellow.Core.ScreenManagement;
using Yellow.Core.Utils;

namespace Yellow.Core.CameraManagement
{
    public class Camera
    {
        private readonly View view;

        private float currentZoom = 1f;

        public Vec2 position;

        public float zoom = 1f;

        public string Name { get; }

        public Camera(Screen screen, string name)
        {
            view = screen.Window.DefaultView;
            Name = name;
        }

        public Camera Move(Vec2 movement)
        {
            position.Add(movement);

            return this;
        }

        public float Zoom(float factor)
        {
            return zoom *= factor;
        }

        public void SetupRenderTarget(RenderTarget target)
        {
            view.Center = position;

            if (zoom != currentZoom)
            {
                view.Zoom(zoom / currentZoom);
                currentZoom = zoom;
            }

            target.SetView(view);
        }
    }
}
