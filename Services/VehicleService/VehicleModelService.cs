using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vehicle_task.Data;
using vehicle_task.DTOs;

namespace vehicle_task.Services.VehicleService
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public VehicleModelService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetVehicleModelDto>>> AddVehicleModel(AddVehicleModelDto newVehicleModel)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleModelDto>>();
            try
            {
                var vehicleModel = _mapper.Map<VehicleModel>(newVehicleModel);
                _context.VehicleModels.Add(vehicleModel);
                vehicleModel = await _context.VehicleModels.FirstOrDefaultAsync(c => c.Id == vehicleModel.Id);
                serviceResponse.Data = new List<GetVehicleModelDto> { _mapper.Map<GetVehicleModelDto>(vehicleModel) };
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to add vehicle model: " + ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVehicleModelDto>>> DeleteVehicleModel(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleModelDto>>();
            try
            {

                var vehicleModel = await _context.VehicleModels.FirstOrDefaultAsync(c => c.Id == id);
                if (vehicleModel is null)
                    throw new Exception($"Vehicle model with Id '{id}' not found.");

                _context.VehicleModels.Remove(vehicleModel);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.VehicleModels
            .Select(c => _mapper.Map<GetVehicleModelDto>(c))
            .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVehicleModelDto>>> GetAllVehicleModels(string searchTerm= null , string sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var serviceResponse = new ServiceResponse<List<GetVehicleModelDto>>();
             try
            {
                var query = _context.VehicleModels.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    query = query.Where(vm => vm.Name.ToLower().Contains(searchTerm) || vm.Abrv.ToLower().Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.ToLower())
                    {
                        case "name":
                            query = ascending ? query.OrderBy(vm => vm.Name) : query.OrderByDescending(vm => vm.Name);
                            break;
                        case "id":
                            query = ascending ? query.OrderBy(vm => vm.Id) : query.OrderByDescending(vm => vm.Id);
                            break;
                        case "abrv":
                            query = ascending ? query.OrderBy(vm => vm.Abrv) : query.OrderByDescending(vm => vm.Abrv);
                            break;
                        default:
                            break;
                    }
                }
                //straničenje
                int skip = (pageNumber - 1) * pageSize;
                int take = pageSize;
                query = query.Skip(skip).Take(take);
                var dbCharacters = await query.ToListAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetVehicleModelDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to retrieve vehicle makes: " + ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVehicleModelDto>> GetVehicleModelById(int id)
        {
            var serviceResponse = new ServiceResponse<GetVehicleModelDto>();
            var dbCharacter = await _context.VehicleModels.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetVehicleModelDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVehicleModelDto>> UpdateVehicleModel(UpdateVehicleModelDto updatedVehicleModel)
        {
             var serviceResponse = new ServiceResponse<GetVehicleModelDto>();

            try
            {
                var vehicleModel = await _context.VehicleModels.FirstOrDefaultAsync(c => c.Id == updatedVehicleModel.Id);

                if (vehicleModel is null)
                    throw new Exception($"Vehicle make with Id '{updatedVehicleModel.Id}' not found.");
                vehicleModel.Name = updatedVehicleModel.Name;
                vehicleModel.Abrv = updatedVehicleModel.Abrv;
                vehicleModel.Make = updatedVehicleModel.Make;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetVehicleModelDto>(updatedVehicleModel);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


    }
}