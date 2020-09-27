using Yellow.Assets.Abstractions;
using SFML.Graphics;
using Yellow.Core.ECS;
using Yellow.Core.Time;
using Yellow.Core.ScreenManagement;
using Yellow.Core.InputManagement;
using Yellow.Core.Systems;
using System;

namespace Yellow.Core
{
    public class Game
    {
        private readonly Screen screen;

        public bool IsRunning { get; private set; }

        public IAssetManager Assets { get; } = Locator.Get<IAssetManager>();

        public TimeManager Time { get; }

        public World World { get; }

        public Renderer Renderer { get; }

        public Input Input { get; }

        public CameraManager Cameras { get; }

        public Game(World world, Input input, Screen screen)
        {
            this.screen = screen;
            screen.WindowClosed += OnWindowClosed;

            Time = new TimeManager(this);
            World = world;
            Input = input;
            Cameras = new CameraManager(screen);
            Renderer = new Renderer(screen);

            World.AddSystem(Cameras);
            World.AddSystem(Renderer);

            World.Prepare();

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
