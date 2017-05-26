using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models.Context;
using TheWorld.Models.Types;
using TheWorld.Services;
using TheWorld.ViewModels.Types;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, 
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }


        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try { 
                var trip= _repository.GetTripByName(tripName);
                
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops {ex}");
            }

            return BadRequest("Failed to get stops!");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stopvm)
        {
            try
            {

                //Check stop is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stopvm);
                    //Lookup latlong
                    var geoCoords = await _coordsService.GetCoordsAsync(newStop.Name);

                    if (!geoCoords.Success)
                    {
                        _logger.LogError(geoCoords.Message);
                    }
                    else
                    {
                        newStop.Latitude = geoCoords.Latitude;
                        newStop.Longitude = geoCoords.Longitude;
                    }

                    newStop.Latitude = geoCoords.Latitude;
                    //save changes to db
                    _repository.AddStop(tripName, newStop);

                    if(await _repository.SaveChangesAsync()) { 
                        return Created($"api/trips/{tripName}/stops/{newStop.Name}", 
                            Mapper.Map<StopViewModel>(newStop));
                    }
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save the stops!! {ex}");
            }

            return BadRequest("Failed to save new stop!");
        }
    }
}
