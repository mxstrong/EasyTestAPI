using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.Entities;
public class ActivationToken : BaseEntity
{
  public string ActivationTokenId { get; set; }
  public string UserId { get; set; }
  public User User { get; set; }
  public DateTime CreatedAt { get; set; }
}
