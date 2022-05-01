using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Web.ApiModels;

public class UserDto
{
  public string UserId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }

  public static UserDto FromUser(User user)
  {
    return new UserDto()
    {
      UserId = user.UserId,
      Name = user.DisplayName,
      Email = user.Email
    };
  }
}
