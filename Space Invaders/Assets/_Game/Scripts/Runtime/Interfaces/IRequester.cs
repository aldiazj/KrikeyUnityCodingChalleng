using Runtime.DI;

namespace Runtime.Interfaces
{
    public interface IRequester
    {
        public void Init(DependencyContainer dependencyContainer);
    }
}