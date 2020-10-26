namespace Yellow.Core.Utils
{
    public interface IPool
    {
        bool IsEmpty { get; }

        void Add(object obj);

        object Get();

        void Populate(int amount);
    }
}
