using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models.Types;
using TheWorld.Models.Context;
using TheWorld.ViewModels.Types;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            try { 
                    var result = _repository.GetAllTrips();
                    return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Getting trip information failed : {ex}");
                return BadRequest("Exception Occured!!");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                
            }

            return BadRequest("Failed to save changes to database!");
        }
    }
}
