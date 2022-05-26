using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace EasyTestAPI.Core.TestAggregate.Specifications;
public class AnsweredTestFullByIdSpec : Specification<AnsweredTest>, ISingleResultSpecification
{
  public AnsweredTestFullByIdSpec(string answeredTestId)
  {
    Query.Where(at => at.AnsweredTestId == answeredTestId).Include(at => at.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers).Include(at => at.TestAnswers);
  }
}
