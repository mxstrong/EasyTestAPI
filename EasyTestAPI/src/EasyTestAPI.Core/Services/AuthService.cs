using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.Entities.Specifications;
using EasyTestAPI.Core.Interfaces;
using EasyTestAPI.SharedKernel.Interfaces;

namespace EasyTestAPI.Core.Services;
public class AuthService : IAuthService
{
  private readonly IRepository<User> _repository;

  public AuthService(IRepository<User> repository)
  {
    _repository = repository;
  }

  public async Task<User?> ActivateUser(string tokenId)
  {
    var user = await _repository.GetBySpecAsync(new UserByTokenSpec(tokenId));
    if (user is null)
    {
      return null;
    }
    user.Activated = true;
    var token = user.ActivationTokens.Find(token => token.ActivationTokenId == tokenId);
    if (token is not null)
    {
      user.ActivationTokens.Remove(token);
    }
    await _repository.SaveChangesAsync();
    return user;
  }

  public async Task<ActivationToken> GenerateActivationToken(string userId)
  {
    var token = new ActivationToken()
    {
      ActivationTokenId = Guid.NewGuid().ToString(),
      UserId = userId,
      CreatedAt = DateTime.UtcNow,
    };

    var user = await _repository.GetByIdAsync(userId);
    if (user.ActivationTokens is null)
    {
      user.ActivationTokens = new List<ActivationToken>();
    }
    user.ActivationTokens.Add(token);

    await _repository.SaveChangesAsync();

    return token;
  }

  public async Task<User?> GetUserByEmail(string email)
  {
    return await _repository.GetBySpecAsync(new UserByEmailSpec(email));
  }

  public async Task<User?> GetUserById(string userId)
  {
    return await _repository.GetByIdAsync(userId);
  }

  public async Task<User?> Login(string email, string password)
  {
    var user = await _repository.GetBySpecAsync(new UserByEmailSpec(email));
    if (user == null)
    {
      return null;
    }

    if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
    {
      return null;
    }

    return user;
  }

  private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
  {
    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
    {
      var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      if (!computedHash.SequenceEqual(passwordHash))
      {
        return false;
      }
    }
    return true;
  }


  public async Task<User> Register(User user, string password)
  {
    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
    user.PasswordHash = passwordHash;
    user.PasswordSalt = passwordSalt;
    user.CreatedAt = DateTime.UtcNow;

    var addedUser = await _repository.AddAsync(user);
    await _repository.SaveChangesAsync();

    return addedUser;
  }

  private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
  {
    using var hmac = new System.Security.Cryptography.HMACSHA512();
    passwordSalt = hmac.Key;
    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
  }

  public async Task<bool> UserExists(string email)
  {
    var user = await _repository.GetBySpecAsync(new UserByEmailSpec(email));
    return user is not null && user.Activated;
  }
}
