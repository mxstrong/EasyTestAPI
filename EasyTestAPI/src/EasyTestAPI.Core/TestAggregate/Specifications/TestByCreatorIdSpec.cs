using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class TestByCreatorIdSpec : Specification<Test>
{
  public TestByCreatorIdSpec(string? userId)
  {
    Query.Where(t => t.CreatedById == userId).Include(test => test.AnsweredTests).ThenInclude(at => at.User);
  }
}
