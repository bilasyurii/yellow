using SFML.Graphics;
using System.Collections.Generic;

namespace Yellow.Assets
{
    public class AssetManager : IAssetManager
    {
        private readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        private readonly Dictionary<string, Atlas> atlases = new Dictionary<string, Atlas>();

        public string root = @"..\..\assets\";

        public AssetManager() {}

        public void AddTexture(string name, Texture texture)
        {
            textures.Add(name, texture);
        }

        public void LoadTexture(string name, string path)
        {
            textures.Add(name, new Texture(root + path));
        }

        public Texture GetTexture(string name)
        {
            return textures[name];
        }

        public void AddAtlas(string name, Atlas atlas)
        {
            atlases.Add(name, atlas);
        }

        public void LoadAtlas(string name, string path)
        {
            // TODO
        }

        public Atlas GetAtlas(string name)
        {
            return atlases[name];
        }

        public Sprite MakeSprite(string name)
        {
            // TODO
            return new Sprite();
        }
    }
}
