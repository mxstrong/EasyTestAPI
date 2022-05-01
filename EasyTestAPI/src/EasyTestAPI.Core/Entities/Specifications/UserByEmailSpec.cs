using Ardalis.Specification;
using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Core.Entities.Specifications;
public class UserByEmailSpec : Specification<User>, ISingleResultSpecification
{
  public UserByEmailSpec(string email)
  {
    Query.Where(t => t.Email == email);
  }
}
