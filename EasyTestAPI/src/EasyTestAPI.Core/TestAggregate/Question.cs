using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.TestAggregate;
public class Question : BaseEntity
{
  public string QuestionId { get; set; }
  public string QuestionText { get; set; }
  public string QuestionTypeId { get; set; }
  public QuestionType QuestionType { get; set; }
}
