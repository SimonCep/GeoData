using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _placeService;
    private readonly IPlaceCreationRequestService _placeCreationRequestService; 

    public PlacesController(IPlaceService placeService, IPlaceCreationRequestService placeCreationRequestService)
    {
        _placeService = placeService;
        _placeCreationRequestService = placeCreationRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PlaceCreationRequest place)
    {
        await _placeCreationRequestService.CreateAsync(place, place.Name, place.Location);
        return Ok();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Place? place = await _placeService.GetAsync(id);
        return Ok(place);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<Place> places = await _placeService.GetAsync();
        return Ok(places);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Place place)
    {
        await _placeService.UpdateAsync(id, place);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _placeService.DeleteAsync(id);
        return Ok();
    }
}