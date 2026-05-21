using DriveFlow.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService adminService;

    public AdminController(IAdminService adminService)
    {
        this.adminService = adminService;
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> Statistics()
        => Ok(await adminService.GetStatisticsAsync());

    [HttpGet("users")]
    public async Task<IActionResult> Users()
        => Ok(await adminService.GetUsersAsync());
}
