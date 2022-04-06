using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class TestWithQuestionsAndAnswersSpec : Specification<Test>
{
  public TestWithQuestionsAndAnswersSpec()
  {
    Query.Include(t => t.Questions)
      .ThenInclude(q => q.Answers)
      .Include(t => t.Questions)
      .ThenInclude(q => q.QuestionType);
  }
}
