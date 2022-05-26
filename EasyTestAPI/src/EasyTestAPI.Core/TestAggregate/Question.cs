using EasyTestAPI.SharedKernel;
using EasyTestAPI.SharedKernel.Interfaces;

namespace EasyTestAPI.Core.TestAggregate;
public class Question : BaseEntity, IAggregateRoot
{
  public string QuestionId { get; set; }
  public string Text { get; set; }
  public string TypeId { get; set; }
  public QuestionType QuestionType { get; set; }
  public string TestId { get; set; }
  public Test Test { get; set; }
  public List<Answer>? Answers { get; set; }
  public List<TestAnswer> TestAnswers { get; set; }
}
