using DriveFlow.Core.DTOs.Auth;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IJwtService jwtService;

    public AuthController(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DrivingLicenseNumber = model.DrivingLicenseNumber
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(user, "User");

        return Ok(new
        {
            Message = "Registration successful."
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password."
            });
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordValid)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password."
            });
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtService.GenerateToken(user, roles);

        var email = user.Email;

        if (email == null)
        {
            return BadRequest(new
            {
                Message = "User email is missing."
            });
        }

        var response = new LoginResponseDto
        {
            Token = token,
            Email = email,
            FullName = $"{user.FirstName} {user.LastName}",
            Roles = roles.ToList()
        };

        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }

        var username = User.Identity.Name;
        var isAuthenticated = User.Identity.IsAuthenticated;

        return Ok(new
        {
            Username = username,
            IsAuthenticated = isAuthenticated
        });
    }
}