using SFML.Graphics;

namespace Yellow.Assets
{
    public interface IAssetManager
    {
        void AddTexture(string name, Texture texture);

        void LoadTexture(string name, string path);

        Texture GetTexture(string name);

        void AddAtlas(string name, Atlas atlas);

        void LoadAtlas(string name, string path);

        Atlas GetAtlas(string name);

        Sprite MakeSprite(string name);
    }
}
