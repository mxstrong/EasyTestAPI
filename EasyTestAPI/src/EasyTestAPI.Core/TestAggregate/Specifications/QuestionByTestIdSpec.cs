using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class QuestionByTestIdSpec : Specification<Question>
{
  public QuestionByTestIdSpec(string testId)
  {
    Query
        .Where(q => q.TestId == testId).Include(q => q.Answers).Include(q => q.QuestionType);
  }
}
