using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class TestByCodeSpec : Specification<Test>, ISingleResultSpecification
{
  public TestByCodeSpec(string code)
  {
    Query.Where(t => t.Code == code).Include(t => t.Questions)
      .ThenInclude(q => q.Answers)
      .Include(t => t.Questions)
      .ThenInclude(q => q.QuestionType);
  }
}
