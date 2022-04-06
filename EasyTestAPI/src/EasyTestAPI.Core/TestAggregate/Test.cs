using EasyTestAPI.Core.Entities;
using EasyTestAPI.SharedKernel;
using EasyTestAPI.SharedKernel.Interfaces;

namespace EasyTestAPI.Core.TestAggregate;

public class Test : BaseEntity, IAggregateRoot
{
  public string TestId { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public string Code { get; set; }
  public string CreatedById { get; set; }
  public User CreatedBy { get; set; }
  public List<Question> Questions { get; set; }
  public List<AnsweredTest> AnsweredTests { get; set; }
}
