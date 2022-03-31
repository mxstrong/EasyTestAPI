using EasyTestAPI.Core.Entities;
using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.TestAggregate;
public class AnsweredTest : BaseEntity
{
  public string AnsweredTestId { get; set; }
  public string TestId { get; set; }
  public Test Test { get; set; }
  public string? UserId { get; set; }
  public User? User { get; set; }
}
