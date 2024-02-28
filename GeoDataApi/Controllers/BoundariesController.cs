using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoundariesController : ControllerBase
{
    private readonly IBoundaryService _boundaryService;

    public BoundariesController(IBoundaryService boundaryService)
    {
        _boundaryService = boundaryService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Boundary boundary)
    {
        await _boundaryService.CreateAsync(boundary);
        return Ok();
    }

    [HttpGet("{placeId}")]
    public async Task<IActionResult> Get(int placeId)
    {
        Boundary? boundary = await _boundaryService.GetAsync(placeId);
        return Ok(boundary);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Boundary> boundaries = await _boundaryService.GetAsync();
        return Ok(boundaries);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Boundary boundary)
    {
        await _boundaryService.UpdateAsync(id, boundary);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _boundaryService.DeleteAsync(id);
        return Ok();
    }
}