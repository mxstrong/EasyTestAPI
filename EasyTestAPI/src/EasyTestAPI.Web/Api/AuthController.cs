using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyTestAPI.Core.Entities;
using EasyTestAPI.Core.Entities.Specifications;
using EasyTestAPI.Core.Interfaces;
using EasyTestAPI.SharedKernel.Interfaces;
using EasyTestAPI.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EasyTestAPI.Web.Api;

public class AuthController : BaseApiController
{
  private readonly IRepository<User> _repository;
  private readonly IEmailSender _emailSender;
  private readonly IAuthService _authService;
  private readonly IConfiguration _configuration;

  public AuthController(IRepository<User> repository, IEmailSender emailSender, IAuthService authService, IConfiguration configuration)
  {
    _repository = repository;
    _emailSender = emailSender;
    _authService = authService;
    _configuration = configuration;
  }

  [HttpGet("user")]
  public async Task<ActionResult<UserDto>> GetCurrentUser()
  {
    var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId is null)
    {
      return NoContent();
    }
    var user = await _repository.GetByIdAsync(userId);
    if (user is null)
    {
      return NoContent();
    }
    return Ok(UserDto.FromUser(user));
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
  {
    var user = await _authService.Login(loginDto.Email, loginDto.Password);

    if (user is null)
    {
      return Unauthorized();
    }

    if (!user.Activated)
    {
      return BadRequest("Kad prisijungtumėte jums reikia aktyvuoti savo paskyrą");
    }

    // generate token
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]{
          new Claim(ClaimTypes.NameIdentifier, user.UserId),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role, "test") // user.Role.Name
        }),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);

    return Ok(new { JWT = tokenString });
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserDto registerDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    var existingUser = await _repository.GetBySpecAsync(new UserByEmailSpec(registerDto.Email));
    if (existingUser is not null)
    {
      return ValidationProblem("User with this email is already registered");
    }
    var userToCreate = new User()
    {
      UserId = registerDto.UserId,
      Email = registerDto.Email,
      DisplayName = registerDto.DisplayName,
      RoleId = "test",
      CreatedAt = registerDto.CreatedAt
    };
    userToCreate.Activated = false;

    var createdUser = await _authService.Register(userToCreate, registerDto.Password);

    var token = await _authService.GenerateActivationToken(createdUser.UserId);

    await _emailSender.SendActivationEmail(createdUser, token);

    return StatusCode(201);
  }

  [HttpGet("activate/{tokenId}")]
  public async Task<IActionResult> Activate(string tokenId)
  {
    var user = await _authService.ActivateUser(tokenId);
    if (user.Activated)
    {
      return Ok();
    }
    return BadRequest("Wrong activation token");
  }
}
