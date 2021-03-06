using Ardalis.Specification;
using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Core.Entities.Specifications;
public class UserByIdSpec : Specification<User>, ISingleResultSpecification
{
  public UserByIdSpec(string userId)
  {
    Query.Where(user => user.UserId == userId);
  }
}
