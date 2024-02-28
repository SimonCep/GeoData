using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Tag tag)
    {
        await _tagService.CreateAsync(tag);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Tag? tag = await _tagService.GetAsync(id);
        return Ok(tag);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int placeId)
    {
        IEnumerable<Tag> tags;
        
        if (placeId != default)
        {
            tags = await _tagService.GetByPlaceIdAsync(placeId);
        }
        else
        {
            tags = await _tagService.GetAsync();
        }

        return Ok(tags);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Tag tag)
    {
        await _tagService.UpdateAsync(id, tag);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _tagService.DeleteAsync(id);
        return Ok();
    }
}