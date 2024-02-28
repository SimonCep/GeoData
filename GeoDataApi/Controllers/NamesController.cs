using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NamesController : ControllerBase
{
    private readonly INameService _nameService;

    public NamesController(INameService nameService)
    {
        _nameService = nameService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Name name)
    {
        await _nameService.CreateAsync(name);
        return Ok();
    }

    [HttpGet("{value}")]
    public async Task<IActionResult> Get(string value)
    {
        Name? name = await _nameService.GetAsync(value);
        return Ok(name);
    }

    [HttpGet("{value}&{locale}")]
    public async Task<IActionResult> Get(string value, string locale)
    {
        IEnumerable<Name> names = await _nameService.GetAsync(value, locale);
        return Ok(names);
    }


    [HttpGet("{placeId}/{locale}")]
    public async Task<IActionResult> Get(int placeId, string locale)
    {
        Name? name = await _nameService.GetAsync(placeId, locale);
        return Ok(name);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int placeId)
    {
        IEnumerable<Name> names;

        if (placeId != default)
        {
            names = await _nameService.GetByPlaceIdAsync(placeId);
        }
        else
        {
            names = await _nameService.GetAsync();
        }
        
        return Ok(names);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Name name)
    {
        await _nameService.UpdateAsync(id, name);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _nameService.DeleteAsync(id);
        return Ok();
    }
}