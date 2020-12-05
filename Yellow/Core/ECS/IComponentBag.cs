using System.Collections;

namespace Yellow.Core.ECS
{
    public interface IComponentBag: IEnumerable
    {
        void Add(Component component);
        void Remove(Component component);
    }
}
