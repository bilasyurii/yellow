using Yellow.Assets.Atlases;
using SFML.Graphics;

namespace Yellow.Assets.Abstractions
{
    public interface IAssetManager
    {
        string Root { get; set; }

        void AddTexture(string name, Texture texture);

        void LoadTexture(string name, string path);

        Texture GetTexture(string name);

        void AddAtlas(Atlas atlas);

        void LoadAtlas(string name, string path, Texture texture);

        Atlas GetAtlas(string name);

        Sprite MakeSprite(string name);

        Sprite MakeSprite(string name, string atlasName);
    }
}
