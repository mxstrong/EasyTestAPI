using System.Text.Json.Serialization;
using EasyTestAPI.SharedKernel;
using EasyTestAPI.SharedKernel.Interfaces;

namespace EasyTestAPI.Core.TestAggregate;
public class QuestionType : BaseEntity, IAggregateRoot
{
  public string QuestionTypeId { get; set; }
  public string Name { get; set; }
  [JsonIgnore]
  public List<Question> Questions { get; set; }
}
