using Yellow.Assets.Atlases;
using Yellow.Assets.JSON;

namespace Yellow.Assets.Abstractions
{
    public interface IAtlasParser
    {
        Atlas Parse(JNode json);
    }
}
