using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class AnsweredTestBySolverIdSpec: Specification<AnsweredTest>
{
  public AnsweredTestBySolverIdSpec(string? userId)
  {
    Query.Where(at => at.UserId == userId).Include(at => at.Test);
  }
}
