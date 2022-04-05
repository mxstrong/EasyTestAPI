using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.TestAggregate;
public class Question : BaseEntity
{
  public string QuestionId { get; set; }
  public string Text { get; set; }
  public string TypeId { get; set; }
  public QuestionType QuestionType { get; set; }
  public List<Answer>? Answers { get; set; }
  public List<TestAnswer> TestAnswers { get; set; }
}
