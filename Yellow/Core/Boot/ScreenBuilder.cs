using SFML.Window;
using Yellow.Core.Utils;

namespace Yellow.Core.Boot
{
    public class ScreenBuilder
    {
        public bool isFullscreen = false;

        public bool isResizable = false;

        public string title = "Yellow";

        public Vec2 size = new Vec2(640, 480);

        public Vec2 position;

        public VideoMode VideoMode => new VideoMode((uint)size.x, (uint)size.y, 32);

        public ScreenBuilder SetFullscreen(bool isFullscreen)
        {
            this.isFullscreen = isFullscreen;

            return this;
        }

        public ScreenBuilder SetResizable(bool isResizable)
        {
            this.isResizable = isResizable;

            return this;
        }

        public ScreenBuilder SetSize(Vec2 size)
        {
            this.size = size;

            return this;
        }

        public ScreenBuilder SetPosition(Vec2 position)
        {
            this.position = position;

            return this;
        }

        public ScreenBuilder SetTitle(string title)
        {
            this.title = title;

            return this;
        }
    }
}
