using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class QuestionTypeByNameSpec : Specification<QuestionType>, ISingleResultSpecification
{
  public QuestionTypeByNameSpec(string name)
  {
    Query
        .Where(qt => qt.Name == name);
  }
}
