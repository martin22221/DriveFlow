using DriveFlow.Core.DTOs.Bookings;
using DriveFlow.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService bookingService;

    public BookingsController(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    [HttpGet("mine")]
    public async Task<IActionResult> Mine()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await bookingService.GetMyBookingsAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var id = await bookingService.CreateAsync(model, userId);
        return Ok(new { Id = id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await bookingService.CancelAsync(id, userId) ? NoContent() : Forbid();
    }
}
