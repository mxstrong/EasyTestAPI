using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.Entities;
public class User : BaseEntity
{
  public string UserId { get; set; }
  public string Email { get; set; }
  public string DisplayName { get; set; }
  public byte[] PasswordHash { get; set; }
  public byte[] PasswordSalt { get; set; }
  public string RoleId { get; set; }
  public Role Role { get; set; }
  public bool Activated { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
