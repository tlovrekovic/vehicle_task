using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vehicle_task.DTOs;
using vehicle_task.Services.VehicleService;

namespace vehicle_task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleMakeController : ControllerBase
    {

        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleMakeDto>>>> Get([FromQuery] string searchTerm = "",
    [FromQuery] string sortBy = "Id",
    [FromQuery] bool ascending = true,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            return Ok(await _vehicleMakeService.GetAllVehicleMakes(searchTerm, sortBy, ascending, pageNumber, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleMakeDto>>> GetSingle(int id)
        {
            return Ok(await _vehicleMakeService.GetVehicleMakeById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetVehicleMakeDto>>> AddVehicleMake(AddVehicleMakeDto newVehicleMake)
        {

            return Ok(await _vehicleMakeService.AddVehicleMake(newVehicleMake));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetVehicleMakeDto>>> UpdateVehicleMake(UpdateVehicleMakeDto updatedVehicleMake)
        {
            var response = await _vehicleMakeService.UpdateVehicleMake(updatedVehicleMake);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleMakeDto>>> DeleteVehicleMake(int id)
        {
            var response = await _vehicleMakeService.DeleteVehicleMake(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
    }
}