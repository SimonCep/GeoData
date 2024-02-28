using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Location location)
    {
        await _locationService.CreateAsync(location);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Location? location = await _locationService.GetAsync(id);
        return Ok(location);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<Location> locations = await _locationService.GetAsync();
        return Ok(locations);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Location location)
    {
        await _locationService.UpdateAsync(id, location);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _locationService.DeleteAsync(id);
        return Ok();
    }
}