using DriveFlow.Core.DTOs.Reviews;
using DriveFlow.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet("car/{carId}")]
    public async Task<IActionResult> ByCar(int carId)
        => Ok(await reviewService.GetByCarIdAsync(carId));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var id = await reviewService.CreateAsync(model, userId);
        return Ok(new { Id = id });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole("Administrator");
        return await reviewService.DeleteAsync(id, userId, isAdmin) ? NoContent() : Forbid();
    }
}
