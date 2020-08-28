using Yellow.Assets.JSON;

namespace Yellow.Assets.Abstractions
{
    public interface IJsonParser
    {
        JNode Parse(string data);

        JNode Result { get; }
    }
}
