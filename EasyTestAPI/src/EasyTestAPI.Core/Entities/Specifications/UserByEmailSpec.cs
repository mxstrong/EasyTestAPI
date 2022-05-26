using Ardalis.Specification;

namespace EasyTestAPI.Core.Entities.Specifications;
public class UserByEmailSpec : Specification<User>, ISingleResultSpecification
{
  public UserByEmailSpec(string email)
  {
    Query.Where(t => t.Email == email);
  }
}
