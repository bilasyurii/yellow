using System.Collections.Generic;
using Yellow.Assets.Abstractions;
using Yellow.Assets.JSON;
using SFML.Graphics;

namespace Yellow.Assets.Atlases
{
    public class AtlasParser : IAtlasParser
    {
        public Atlas Parse(JNode json)
        {
            var regions = new Dictionary<string, IntRect>();
            var frames = json["frames"].Dictionary;

            Dictionary<string, JNode> frame;
            
            foreach (var record in frames)
            {
                frame = record.Value["frame"].Dictionary;

                regions.Add(record.Key, new IntRect(frame["x"].Integer, frame["y"].Integer, frame["w"].Integer, frame["h"].Integer));
            }

            return new Atlas()
            {
                regions = regions
            };
        }
    }
}
