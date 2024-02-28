using GeoDataBusiness.Services;
using GeoDataInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceCreationRequestsController : ControllerBase
{
    private readonly IPlaceCreationRequestService _placeCreationRequestService;

    public PlaceCreationRequestsController(IPlaceCreationRequestService placeCreationRequestService)
    {
        _placeCreationRequestService = placeCreationRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PlaceCreationRequest place)
    {
        await _placeCreationRequestService.CreateAsync(place, place.Name, place.Location);
        return Ok();
    }
}
