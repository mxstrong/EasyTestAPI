using Ardalis.Specification;

namespace EasyTestAPI.SharedKernel.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
