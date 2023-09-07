global using vehicle_task.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_task.DTOs;

namespace vehicle_task.Services.VehicleService
{
    public interface IVehicleMakeService
    {
         Task<ServiceResponse<List<GetVehicleMakeDto>>>GetAllVehicleMakes(string searchTerm, string sortBy, bool ascending, int pageNumber, int pageSize);
        Task<ServiceResponse<GetVehicleMakeDto>> GetVehicleMakeById(int id);
        Task<ServiceResponse<List<GetVehicleMakeDto>>> AddVehicleMake (AddVehicleMakeDto newVehicleMake);

        Task<ServiceResponse<GetVehicleMakeDto>> UpdateVehicleMake (UpdateVehicleMakeDto updatedVehicleMake);

        Task<ServiceResponse<List<GetVehicleMakeDto>>>DeleteVehicleMake(int id);
    }
}