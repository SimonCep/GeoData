using GeoDataApi.Services;
using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IHashService _hashService;
    private readonly IUserService _userService;

    public record RegisterRequest(string Email, string Password);
    public record LoginRequest(string Email, string Password);

    public AuthenticationController(IAuthenticationService authenticationService, IHashService hashService, IUserService userService)
    {
        _authenticationService = authenticationService;
        _hashService = hashService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var user = new User
        {
            Email = registerRequest.Email,
            HashedPassword = _hashService.ComputeSHA256(registerRequest.Password)
        };

        await _userService.CreateAsync(user);
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.GetAsync(loginRequest.Email);

        if (user is null)
        {
            return BadRequest();
        }

        var passwordMatch = _hashService.ComputeSHA256(loginRequest.Password) == user.HashedPassword;

        if (!passwordMatch)
        {
            return BadRequest();
        }

        var token = _authenticationService.CreateToken(loginRequest.Email);

        return Ok(new { token });
    }
}