using SFML.Graphics;
using System.Collections.Generic;

namespace Yellow.Assets.Atlases
{
    public class Atlas
    {
        public string name;

        public Texture texture;

        public Dictionary<string, IntRect> regions;
    }
}
