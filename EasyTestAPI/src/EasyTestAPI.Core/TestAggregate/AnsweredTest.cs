using EasyTestAPI.Core.Entities;
using EasyTestAPI.SharedKernel;
using EasyTestAPI.SharedKernel.Interfaces;

namespace EasyTestAPI.Core.TestAggregate;
public class AnsweredTest : BaseEntity, IAggregateRoot
{
  public string AnsweredTestId { get; set; }
  public string TestId { get; set; }
  public Test Test { get; set; }
  public string? UserId { get; set; }
  public User? User { get; set; }
  public List<TestAnswer> TestAnswers { get; set; }
  public DateTime SolvedAt { get; set; }
}
