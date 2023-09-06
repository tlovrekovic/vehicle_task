using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_task.DTOs;

namespace vehicle_task.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        public Task<ServiceResponse<List<GetVehicleModelDto>>> AddVehicleModel(AddVehicleModelDto newVehicleModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetVehicleModelDto>>> DeleteVehicleModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetVehicleModelDto>>> GetAllVehicleModels()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetVehicleModelDto>> GetVehicleModelById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetVehicleModelDto>> UpdateVehicleModel(UpdateVehicleModelDto updatedVehicleModel)
        {
            throw new NotImplementedException();
        }
    }
}