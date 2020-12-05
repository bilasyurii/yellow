using Yellow.Assets.Abstractions;
using Yellow.Assets.Atlases;
using Yellow.Assets.JSON;
using Yellow.Core;
using Yellow.Core.Boot;
using Yellow.Core.Components;

namespace Template
{
    public class Startup : IStartup
    {
        public Game Game { get; set; }

        public void ProvideServices()
        {
            Locator.ProvideStandardServices();
            Locator.Provide<IAtlasParser>(new AtlasParser());
            Locator.Provide<IJsonParser>(new JsonParser());
        }

        public void PreloadAssets()
        {
            var assetManager = Game.Assets;

            assetManager.LoadTexture("robot", @"img\spritelist.png");
            assetManager.LoadAtlas("robotAtlas", @"atlas\atlas01.json", assetManager.GetTexture("robot"));
        }

        public void Configure(Configuration configuration)
        {
            configuration
                .ConfigureScreen((builder) => builder
                    .SetResizable(false)
                    .SetTitle("Yellow template")
                )
                .ConfigureWorld((builder) => builder
                    .SetEntitiesPoolSize(100)
                );
        }

        public void Prepare()
        {
            var game = Game;

            var sprite = game.MakeSprite("tile");

            var entity = game.MakeGameObject();
            var graphic = entity.Graphic = game.World.CreateComponent<Graphic>();

            graphic.drawable = sprite;

            var transform = entity.Transform;
            
            transform.Translate(100f, 100f);
            transform.SetPivot(35f, 19f);

            game.World.Add(entity);

            game.Time.Events.Repeat(1000, 1, (args) => System.Console.WriteLine("Hello, Yellow!"));

            game.Time.Events.Loop(500, (args) => System.Console.WriteLine("Hello!"));

            var camera = game.Cameras.Active;

            game.Time.Events.Loop(20, (args) => camera.owner.Transform.Translate(1f, 0f));
        }

        public void OnGameEnded()
        {
            System.Console.WriteLine("Bye, Yellow!");
        }
    }
}
