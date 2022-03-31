using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.TestAggregate;
public class TestAnswer : BaseEntity
{
  public string TestAnswerId { get; set; }
  public string AnsweredTestId { get; set; }
  public AnsweredTest AnsweredTest { get; set; }
  public string Answer { get; set; }
  public string QuestionId { get; set; }
  public Question Question { get; set; }
}
