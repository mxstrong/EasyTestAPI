using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.TestAggregate;
public class Answer : BaseEntity
{
  public string AnswerId { get; set; }
  public string AnswerText { get; set; }
  public string QuestionId { get; set; }
  public Question Question { get; set; }
}
