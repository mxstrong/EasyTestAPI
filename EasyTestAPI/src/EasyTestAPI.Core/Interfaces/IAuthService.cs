using System;
using System.Collections.Generic;
using System.Linq;
using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.Entities;

namespace EasyTestAPI.Core.Interfaces;
public interface IAuthService
{
  Task<User> Register(User user, string password);
  Task<ActivationToken> GenerateActivationToken(string UserId);
  Task<User?> ActivateUser(string tokenId);
  Task<User> Login(string email, string password);
  Task<bool> UserExists(string email);
  Task<User?> GetUserByEmail(string email);
  Task<User?> GetUserById(string userId);
}
