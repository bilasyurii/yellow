using System.Collections;

namespace Yellow.Core.ECS
{
    public interface IComponentBag: IEnumerable
    {
        void Add(IComponent component);
        void Remove(IComponent component);
    }
}
