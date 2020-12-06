using SFML.Graphics;
using Yellow.Core.ECS;

namespace Yellow.Core.Components
{
    public class Graphic: BaseComponent
    {
        public Drawable drawable;

        public Graphic() {}

        public Graphic(Drawable drawable)
        {
            this.drawable = drawable;
        }
    }
}
