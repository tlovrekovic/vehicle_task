using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vehicle_task.DTOs;
using vehicle_task.Parameters;
using vehicle_task.Services;

namespace vehicle_task.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VehicleModelController : ControllerBase
    {

        private readonly IVehicleModelService _vehicleMakeService;

        public VehicleModelController(IVehicleModelService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleModelDto>>>> Get(
            [FromQuery] PagingParameters pagingParameters,
    [FromQuery] SortingParameters sortingParameters,
    [FromQuery] FilteringParameters filteringParameters)
        {
            return Ok(await _vehicleMakeService.GetAllVehicleModels(pagingParameters, sortingParameters, filteringParameters));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleModelDto>>> GetSingle(int id)
        {
            return Ok(await _vehicleMakeService.GetVehicleModelById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetVehicleModelDto>>> AddVehicleMake(AddVehicleModelDto newVehicleModel)
        {

            return Ok(await _vehicleMakeService.AddVehicleModel(newVehicleModel));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetVehicleModelDto>>> UpdateVehicleMake(UpdateVehicleModelDto updatedVehicleModel)
        {
            var response = await _vehicleMakeService.UpdateVehicleModel(updatedVehicleModel);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleModelDto>>> DeleteVehicleModel(int id)
        {
            var response = await _vehicleMakeService.DeleteVehicleModel(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
    }
}