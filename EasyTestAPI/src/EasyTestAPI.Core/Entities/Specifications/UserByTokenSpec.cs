using Ardalis.Specification;
using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Core.Entities.Specifications;
public class UserByTokenSpec : Specification<User>, ISingleResultSpecification
{
  public UserByTokenSpec(string tokenId)
  {
    Query.Where(t => t.ActivationTokens.Any(token => token.ActivationTokenId == tokenId)).Include(u => u.ActivationTokens);
  }
}
