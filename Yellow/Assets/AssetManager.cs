using Yellow.Assets.Abstractions;
using Yellow.Assets.Atlases;
using SFML.Graphics;
using System.Collections.Generic;
using Yellow.Core;
using System.IO;
using System;

namespace Yellow.Assets
{
    public class AssetManager : IAssetManager
    {
        private readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        private readonly Dictionary<string, Atlas> atlases = new Dictionary<string, Atlas>();

        private IAtlasParser atlasParser = null;

        private IJsonParser jsonParser = null;

        private IAtlasParser AtlasParser
        {
            get
            {
                return atlasParser ??= Locator.Get<IAtlasParser>();
            }
        }

        private IJsonParser JsonParser
        {
            get
            {
                return jsonParser ??= Locator.Get<IJsonParser>();
            }
        }

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

        public void LoadAtlas(string name, string path, Texture texture)
        {
            var data = ReadFullFile(path);
            var json = JsonParser.Parse(data);
            var atlas = AtlasParser.Parse(json);

            atlas.name = name;
            atlas.texture = texture;

            atlases.Add(name, atlas);
        }

        public Atlas GetAtlas(string name)
        {
            return atlases[name];
        }

        public Sprite MakeSprite(string name)
        {
            Atlas atlas;

            foreach (var record in atlases)
            {
                atlas = record.Value;

                if (atlas.regions.TryGetValue(name, out var rect))
                {
                    return new Sprite(atlas.texture, rect);
                }
            }

            if (textures.TryGetValue(name, out var texture))
            {
                return new Sprite(texture);
            }

            Console.WriteLine($"Couldn't find atlas region or texture with name {name}.");

            // TODO return some default image

            return new Sprite();
        }

        public Sprite MakeSprite(string name, string atlasName)
        {
            var atlas = atlases[atlasName];
            
            return new Sprite(atlas.texture, atlas.regions[name]);
        }

        public string ReadFullFile(string path, string root = null)
        {
            if (root == null)
            {
                path = this.root + path;
            }

            FileStream stream = null;
            StreamReader reader = null;
            string data;

            try
            {
                stream = new FileStream(path, FileMode.Open);
                reader = new StreamReader(stream);
                data = reader.ReadToEnd();
            }
            catch(Exception e)
            {
                if (stream != null)
                {
                    stream.Dispose();
                }

                if (reader != null)
                {
                    reader.Dispose();
                }

                throw e;
            }

            return data;
        }
    }
}
