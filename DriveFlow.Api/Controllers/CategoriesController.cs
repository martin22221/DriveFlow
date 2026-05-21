using DriveFlow.Core.DTOs.Categories;
using DriveFlow.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await categoryService.GetAllAsync());

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto model)
    {
        var id = await categoryService.CreateAsync(model);
        return Ok(new { Id = id });
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await categoryService.DeleteAsync(id) ? NoContent() : NotFound();
}
