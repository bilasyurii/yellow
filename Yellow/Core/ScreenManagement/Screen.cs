using SFML.Graphics;
using SFML.Window;
using System;
using Yellow.Core.Boot;

namespace Yellow.Core.ScreenManagement
{
    public class Screen
    {
        public RenderWindow Window { get; }

        public event EventHandler WindowClosed;

        public Screen(ScreenBuilder builder)
        {
            var styles = Styles.Default;

            if (builder.isFullscreen)
            {
                styles |= Styles.Fullscreen;
            }

            if (!builder.isResizable)
            {
                styles &= ~Styles.Resize;
            }

            Window = new RenderWindow(builder.VideoMode, builder.title, styles)
            {
                Position = builder.position
            };

            Window.Closed += OnWindowClosed;
        }

        public void Update()
        {
            Window.DispatchEvents();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            Window.Close();

            WindowClosed(this, e);
        }
    }
}
