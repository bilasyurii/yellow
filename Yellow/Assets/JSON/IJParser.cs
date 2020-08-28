namespace Yellow.Assets.JSON
{
    public interface IJParser
    {
        JNode Parse(string data);

        JNode Result { get; }
    }
}
