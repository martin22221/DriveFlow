using DriveFlow.Core.DTOs.Cars;
using DriveFlow.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService carService;

    public CarsController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CarQueryDto query)
    {
        var cars = await carService.GetAllAsync(query);
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await carService.GetByIdAsync(id);
        return car == null ? NotFound() : Ok(car);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCarDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var id = await carService.CreateAsync(model, userId);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCarDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole("Administrator");

        var success = await carService.UpdateAsync(id, model, userId, isAdmin);
        return success ? NoContent() : Forbid();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole("Administrator");

        var success = await carService.DeleteAsync(id, userId, isAdmin);
        return success ? NoContent() : Forbid();
    }
}
