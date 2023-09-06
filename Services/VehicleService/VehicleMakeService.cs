using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_task.DTOs;

namespace vehicle_task.Services.VehicleService
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public Task<ServiceResponse<List<GetVehicleMakeDto>>> AddVehicleMake(AddVehicleMakeDto newVehicleMake)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetVehicleMakeDto>>> DeleteVehicleMake(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetVehicleMakeDto>>> GetAllVehicleMakes()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetVehicleMakeDto>> GetVehicleMakeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetVehicleMakeDto>> UpdateVehicleMake(UpdateVehicleMakeDto updatedVehicleMake)
        {
            throw new NotImplementedException();
        }
    }
}