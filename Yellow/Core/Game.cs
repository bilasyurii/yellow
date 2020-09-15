using Yellow.Assets.Abstractions;
using SFML.Graphics;
using Yellow.Core.ECS;
using Yellow.Core.Time;
using Yellow.Core.Rendering;
using Yellow.Core.ScreenManagement;
using Yellow.Core.InputManagement;
using System;

namespace Yellow.Core
{
    public class Game
    {
        private readonly Screen screen;

        public bool IsRunning { get; private set; }

        public IAssetManager Assets { get; } = Locator.Get<IAssetManager>();

        public TimeManager Time { get; private set; }

        public World World { get; private set; }

        public Renderer Renderer { get; private set; }

        public Input Input { get; private set; }

        public Game(World world, Input input, Screen screen)
        {
            this.screen = screen;
            screen.WindowClosed += OnWindowClosed;

            Time = new TimeManager(this);
            World = world;
            Input = input;
            Renderer = new Renderer(screen);
            World.AddSystem(Renderer);

            Locator.Provide(this);
        }

        public Sprite MakeSprite(string name)
        {
            return Assets.MakeSprite(name);
        }

        public Sprite MakeSprite(string name, string atlas)
        {
            return Assets.MakeSprite(name, atlas);
        }

        public Entity MakeEntity()
        {
            return World.CreateEntity();
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;

                while (IsRunning)
                {
                    var dt = Time.Update();
                    Input.Update(dt);
                    screen.Update();
                    World.Update();
                    Renderer.Render();
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            IsRunning = false;
        }
    }
}
