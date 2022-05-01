using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Web.ApiModels;

public class RegisterUserDto
{
  public string UserId = Guid.NewGuid().ToString();
  public string DisplayName { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public bool Activated = false;
  public DateTime CreatedAt = DateTime.UtcNow;
}
